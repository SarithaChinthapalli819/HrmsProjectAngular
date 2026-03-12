import { Component, OnInit } from '@angular/core';
import { TeamsService } from '../core/teams.service';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html'
})
export class TeamsComponent implements OnInit{
  data:any;
  constructor(private teamsService:TeamsService){
    
  }
  teamsData = [];
  isMembers:boolean = false;
  teamId : any;
  ngOnInit(): void {
    this.getTeams();
  }
  getTeams(){
    this.teamsService.getTeams().subscribe((response:any)=>{
      if(response && response.success){
        this.teamsData = response.data;
      }
    })
  }
  isAdd: boolean = false;
  addOrEditTeam(item){
    this.isAdd = true;
    this.data = item;
  }
  isChanged(event){
    this.isAdd = false;
    if(event){
      this.getTeams();
    }
  }

  openMembers(item){
    this.teamId = item.teamId;
    this.isMembers = true;
  }
  popupClosed(event){
    this.isMembers = false;
    this.getTeams();
  }
}
