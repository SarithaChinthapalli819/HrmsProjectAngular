import { NgModule } from "@angular/core";
import { EmployeeComponent } from "./employee.component";
import { RouterModule } from "@angular/router";
import { EmployeeFormComponent } from './employee-form/employee-form.component';
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
const routes = [{
    path:'',component:EmployeeComponent
}]
@NgModule({
    declarations:[EmployeeComponent, EmployeeFormComponent],
    imports:[RouterModule.forChild(routes),CommonModule,FormsModule,ReactiveFormsModule]

})
export class Emmployemodule{

}