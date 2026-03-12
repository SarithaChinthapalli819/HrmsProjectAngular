import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { TimesheetComponent } from "./timesheet.component";
import { CommonModule } from "@angular/common";
const routes = [{
    path:'',component:TimesheetComponent
}]
@NgModule({
    declarations:[TimesheetComponent],
    imports:[RouterModule.forChild(routes),CommonModule]

})
export class Timesheetmodule{

}