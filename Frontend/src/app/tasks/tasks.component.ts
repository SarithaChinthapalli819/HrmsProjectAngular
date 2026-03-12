import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { TasksService } from '../core/tasks.service';
import { TasksModel } from '../Models/tasks.model';
import { ToastrService } from 'ngx-toastr';
import { TaskSpenTimeModel } from '../Models/task-spenTime-model';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html'
})
export class TasksComponent implements OnInit {
  constructor(private tasksService: TasksService, private taost: ToastrService) {

  }
  isTaskFormOpened: boolean = false;
  tasks = [];
  data :any;
  columns = [
    { id: 0, name: 'Backlog', tasks: [] },
    { id: 1, name: 'In Progress', tasks: [] },
    { id: 2, name: 'Review', tasks: [] },
    { id: 3, name: 'Done', tasks: [] }
  ]
  popupClosed(event) {
    this.isTaskFormOpened = false;
    if(event){
      this.getTasks();
    }
  }
  ngOnInit(): void {
    this.getTasks();
  }
  priority = [
    { name: 'Low', value: 1 },
    { name: 'Medium', value: 2 },
    { name: 'High', value: 3 }
  ]

  getTasks() {
    var model = new TasksModel();
    model.userId = localStorage.getItem("userId");
    this.tasksService.getTask(model).subscribe((response: any) => {
      if (response.success && response.data && response.data.length > 0) {
        response.data.forEach(response => {
          var isRunningTask = response.startTime != null ? true : false;
          this.columns.find(item => item.id == response.status)?.tasks?.push({ ...response, isRunningTask:isRunningTask  });
        }) 
      }
    })
  }

  getPriorityBadge(p: number): string {
    switch (p) {
      case 3:
        return 'bg-rose-50 text-rose-600';
      case 2:
        return 'bg-amber-50 text-amber-600';
      case 1:
        return 'bg-emerald-50 text-emerald-600';
      default:
        return 'bg-slate-100 text-slate-600';
    }
  }

  getPriority(value) {
    return this.priority.find((item: any) => item.value == value).name;
  }

  upsertSpentTime(task) {
    var model = new TaskSpenTimeModel();
    model.TaskId = task.taskId;
    model.UserId = localStorage.getItem("userId");
    if (!task.isRunningTask) {
      model.StartTime = new Date();
      model.EndTime = null;
    }
    else {
      model.StartTime = task.startTime;
      model.EndTime = new Date();
    }
    this.tasksService.upsertSpenTime(model).subscribe((response: any) => {
      if (response.success) {
        this.tasksService.getSpenTime(model).subscribe((response:any)=>{
          if(response.success){
            task.startTime = !task.isRunningTask ? response.data.startTime : null;
             this.columns.forEach(response => {
              response.tasks.forEach((item:any)=>{
                if(item !=task){
                  item.isRunningTask = false;
                }
              })
              })
             task.isRunningTask = !task.isRunningTask;
          }
        }) 
      }
    })

  }


  drop(event: CdkDragDrop<TasksModel[]>) {
    if (event.previousContainer == event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      const task = event.previousContainer.data[event.previousIndex];
      const newStatus = this.columns.find(c => c.tasks === event.container.data)?.id || 0;

      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex,
      );
      console.log(task)
      var model = new TasksModel();
      model.isUpdateStatus = true;
      model.taskId = task.taskId;
      model.status = newStatus;
      this.tasksService.upsertTask(model).subscribe((response: any) => {
        if (response.success) {
          this.taost.success('Status updated successfully...')
        }
      })
    }

  }

  editClicked(task){
    this.isTaskFormOpened = true;
    this.data = task;
  }
}
