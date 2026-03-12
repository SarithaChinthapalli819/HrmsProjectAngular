import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AuthService } from '../../core/auth.serive';
import { Teams } from '../../Models/Teams.model';
import { TeamsService } from '../../core/teams.service';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-team-members',
    templateUrl: './team-members.component.html',
    standalone: false
})
export class TeamMembersComponent {
  constructor(private authService: AuthService,private teamService : TeamsService,private toaster:ToastrService) {

  }
  userDetails = [];
  userId : any;
  teamId :any;
  teamMembers = [];
  membersCount = 0;
  teamName : any;
  deleteMember : boolean = false;
  isAdd : boolean = false;
  @Output()
  isPopupClosed = new EventEmitter();
  @Input()
  set _teamId(data){
    this.teamId = data;
    console.log(this.teamId)
  }
  ngOnInit() {
    this.getUsers();
    this.getTeammembers();
  }
  getUsers() {
    this.authService.getUserDetails().subscribe((response: any) => {
      this.userDetails = response.data;
    })
  }

  upsertTeammembers(){
    var inputModel = new Teams();
    inputModel.teamId = this.teamId;
    inputModel.teamMemberId = this.userId;
    inputModel.deleteMember = this.deleteMember;
    this.teamService.upsertTeammebers(inputModel).subscribe((response:any)=>{
      if(response.success){
        this.toaster.success('Member addded successfully..');
        this.userId = null;
        this.isAdd = false;
        this.deleteMember = false;
        this.getTeammembers();
      }
    })
  }

  getTeammembers(){
    var inputModel = new Teams();
    inputModel.teamId = this.teamId;
    this.teamService.getTeammebers(inputModel).subscribe((response:any)=>{
      if(response.success){
       this.teamMembers = response.data; 
       console.log(this.teamMembers)
       this.membersCount = this.teamMembers.length;
       this.teamName = this.teamMembers[0].teamName;
      }
    })
  }
  popupClosed(event){
    this.isPopupClosed.emit(event);
  }
}
