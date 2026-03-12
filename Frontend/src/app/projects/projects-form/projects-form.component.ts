import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TeamsService } from '../../core/teams.service';
import { FormControl, FormGroup } from '@angular/forms';
import { ProjectsModel } from '../../Models/projects.model';
import { ProjectService } from '../../core/project.service';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-projects-form',
    templateUrl: './projects-form.component.html',
    standalone: false
})
export class ProjectsFormComponent implements OnInit {
  constructor(private teamService: TeamsService, private projectService: ProjectService, private toast: ToastrService) {

  }
  projectForm: FormGroup;
  ngOnInit(): void {
    this.getTeams();
  }
  @Output()
  popupClosed = new EventEmitter<boolean>(false);
  @Input()
  set data(data) {
    this.setProjectForm();
    if (data) {
      this.projectForm.patchValue({
        projectId: data.projectId,
        projectName: data.projectName,
        clientName: data.clientName,
        priority: data.priority,
        description: data.description,
        teamId: data.teamId,
        startDate: data.startDate,
        budget: data.budget,
        status: data.status
      })
    }
  }
  teams = [];
  priority = [
    { name: 'Low', value: 1 },
    { name: 'Medium', value: 2 },
    { name: 'High', value: 3 }
  ]

  status = [
    { name: 'Planning', value: 1 },
    { name: 'Active', value: 2 },
    { name: 'On hold', value: 3 },
    { name: 'Completed', value: 4 }
  ]
  onCancel(event) {
    this.popupClosed.emit(event);
  }
  getTeams() {
    this.teamService.getTeams().subscribe((response: any) => {
      this.teams = response.data;
    })
  }

  setProjectForm() {
    this.projectForm = new FormGroup({
      projectId: new FormControl(''),
      projectName: new FormControl(''),
      clientName: new FormControl(''),
      priority: new FormControl(''),
      description: new FormControl(''),
      teamId: new FormControl(''),
      startDate: new FormControl(''),
      budget: new FormControl(''),
      status: new FormControl('')
    })
  }

  upsertProject() {
    var project = new ProjectsModel();
    project.ProjectId = this.projectForm.get('projectId').value;
    project.ProjectName = this.projectForm.get('projectName').value;
    project.ClientName = this.projectForm.get('clientName').value;
    project.Priority = this.projectForm.get('priority').value;
    project.Description = this.projectForm.get('description').value;
    project.TeamId = this.projectForm.get('teamId').value;
    project.StartDate = this.projectForm.get('startDate').value;
    project.Budget = this.projectForm.get('budget').value;
    project.Status = this.projectForm.get('status').value;
    this.projectService.upsertProject(project).subscribe((response: any) => {
      if (response.success) {
        this.toast.success('Project Added Successfully..');
        this.popupClosed.emit(true)
      }
    })
  }
}
