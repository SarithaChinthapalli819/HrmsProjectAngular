import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router"; 
import { ProjectsComponent } from "./projects.component";
import { ProjectsFormComponent } from './projects-form/projects-form.component';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
const routes = [
    { path:'',component:ProjectsComponent }
]
@NgModule({
    declarations:[ProjectsComponent, ProjectsFormComponent],
    imports:[RouterModule.forChild(routes),FormsModule,CommonModule,ReactiveFormsModule]

})
export class Projectmodule{

}