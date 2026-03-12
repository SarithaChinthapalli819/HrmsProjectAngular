import { NgModule } from "@angular/core"; 
import { RouterModule } from "@angular/router";
import { AttendanceComponent } from "./attendance.component";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
const routes = [{
    path:'',component:AttendanceComponent
}]
@NgModule({
    declarations:[AttendanceComponent],
    imports:[RouterModule.forChild(routes),CommonModule,FormsModule,ReactiveFormsModule]

})
export class Attendancemodule{

}