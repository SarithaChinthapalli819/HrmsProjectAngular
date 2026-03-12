import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../core/project.service';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html' 
})
export class ProjectsComponent implements OnInit{
  constructor(private projectService: ProjectService){

  }
  priority = [
    {name:'Low',value:1},
    {name:'Medium',value:2},
    {name:'High',value:3}
  ]

  status = [
    {name:'Planning',value:1},
    {name:'Active',value:2},
    {name:'On hold',value:3},
    {name:'Completed',value:4}
  ]
  ngOnInit(): void {
    this.getProjects();
  }
  openAddproject : boolean = false;
  projects = [];
  data : any;
  popupClosed(event){
    this.openAddproject = false;
    if(event){
      this.getProjects();
    }
  }
  getProjects(){
    this.projectService.getProject().subscribe((response:any)=>{
      this.projects = response.data;
    })
  }
  getPriority(item){
    return this.priority.find((priority)=>priority.value == item.priority).name;
  }
  getStatus(item){
       return this.status.find((status)=>status.value == item.status).name;
  }
  getStyles(item){
    var status = this.status.find((status)=>status.value == item.status).name;
    if(status === 'Active' || status === 'Completed'){
      return 'bg-emerald-500/10 text-emerald-600';
    }
    else if(status === 'Planning'){
      return 'bg-amber-500/10 text-amber-600';
    }
    else{
      return 'bg-rose-500/10 text-rose-600';
    }
  }

  onEdit(item){
    this.openAddproject = true;
    this.data = item;
  }
  
}
