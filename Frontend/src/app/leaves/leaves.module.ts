import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { LeavesComponent } from "./leaves.component";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { LeaveFormComponent } from './leave-form/leave-form.component';
const routes = [{
    path:'',component:LeavesComponent
}]
@NgModule({
    declarations:[LeavesComponent, LeaveFormComponent],
    imports:[RouterModule.forChild(routes),CommonModule,FormsModule,ReactiveFormsModule]

})
export class Leavesmodule{

}