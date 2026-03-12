import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { TeamsComponent } from "./teams.component";
import { TeamFormComponent } from './team-form/team-form.component';
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { TeamMembersComponent } from './team-members/team-members.component';
const routes = [{
    path:'',component:TeamsComponent
}]
@NgModule({
    declarations:[TeamsComponent, TeamFormComponent, TeamMembersComponent],
    imports:[RouterModule.forChild(routes),CommonModule,ReactiveFormsModule,FormsModule]

})
export class Teamsmodule{

}