import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router"; 
import { TasksComponent } from "./tasks.component";
import { TaskFormComponent } from './task-form/task-form.component';
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule } from "@angular/forms";
import { DragDropModule } from "@angular/cdk/drag-drop";
const routes = [
    { path:'',component:TasksComponent }
]
@NgModule({
    declarations:[TasksComponent, TaskFormComponent],
    imports:[RouterModule.forChild(routes),CommonModule,ReactiveFormsModule,DragDropModule]

})
export class Tasksmodule{

}