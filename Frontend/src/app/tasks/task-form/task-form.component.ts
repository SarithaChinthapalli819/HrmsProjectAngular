import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { TasksService } from '../../core/tasks.service';
import { ProjectService } from '../../core/project.service';
import { TasksModel } from '../../Models/tasks.model';
import { ToastrService } from 'ngx-toastr';
import { Teams } from '../../Models/Teams.model';
import { TeamsService } from '../../core/teams.service';

@Component({
  selector: 'app-task-form',
  templateUrl: './task-form.component.html'
})
export class TaskFormComponent implements OnInit{
  
  constructor(private taskService:TasksService,private projectService:ProjectService,private toaster:ToastrService,private teamService : TeamsService){

  }
  teamMembers = [];
  @Output()
  popupClosed = new EventEmitter<boolean>();
  tasksForm  : FormGroup;
  projects = [];
  formData : any;
  @Input()
  set data(data: any){
    this.setForm();
    if(data){
       this.formData = data;
    }
  }
  ngOnInit(): void {
    this.getProjects();
    this.tasksForm.get('projectId').valueChanges.subscribe((response:any)=>{
      var teamId = this.projects.find(item => item.projectId === response).teamId;
      this.getTeammembers(teamId);
    })
  }
  setForm(){
    this.tasksForm = new FormGroup({
      taskId : new FormControl(null),
      taskName : new FormControl(''),
      description : new FormControl(''),
      priority : new FormControl(''),
      dueDate : new FormControl(''),
      projectId : new FormControl(null),
      assignedTo : new FormControl('')
    })
  }
  closeForm(event){
    this.popupClosed.emit(event);
  }
   priority = [
    { name: 'Low', value: 1 },
    { name: 'Medium', value: 2 },
    { name: 'High', value: 3 }
  ]
  getProjects(){
    this.projectService.getProject().subscribe((response:any)=>{
      if(response.success && response.data){
        this.projects = response.data;
        var data = this.formData;
        this.tasksForm.patchValue({
          taskId : data.taskId,
          taskName : data.taskName,
          description : data.description,
          priority : data.priority,
          dueDate : data.dueDate ? data.dueDate.split('T')[0] : '',
          projectId : data.projectId,
          assignedTo : data.assignedTo
        });
      }
    })
  }

   
  getTeammembers(teamId){
    var inputModel = new Teams();
    inputModel.teamId = teamId;
    this.teamService.getTeammebers(inputModel).subscribe((response:any)=>{
      if(response.success){
        this.teamMembers = response.data; 
      }
    })
  }
    
  upsertTask(){
    var model = new TasksModel();
    model.projectId = this.tasksForm.get('projectId').value;
    model.taskId = this.tasksForm.get('taskId').value;
    model.taskName = this.tasksForm.get('taskName').value;
    model.priority = this.tasksForm.get('priority').value;
    model.description = this.tasksForm.get('description').value;
    model.dueDate = this.tasksForm.get('dueDate').value;
    model.assignedTo = this.tasksForm.get('assignedTo').value;
    model.isUpdateStatus = false;
    this.taskService.upsertTask(model).subscribe((response:any)=>{
      if(response.success){
        this.toaster.success('Task Added succeesfully..');
        this.closeForm(true);
      }
    })
  }
}
