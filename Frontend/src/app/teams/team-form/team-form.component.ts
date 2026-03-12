import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AuthService } from '../../core/auth.serive';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Teams } from '../../Models/Teams.model';
import { TeamsService } from '../../core/teams.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-team-form',
  templateUrl: './team-form.component.html'
})
export class TeamFormComponent implements OnInit {
  constructor(private authService: AuthService,private teamsService : TeamsService,private toaster:ToastrService) {

  }
  userDetails = [];
  teamsForm: FormGroup;
  isSuccess : boolean = false;
  @Input()
  set data(data){
    this.setTeamsForm();
    if(data != null){
      this.teamsForm.patchValue({
          teamId: data.teamId,
          teamAdminId: data.teamAdminId,
          description: data.description,
          teamName: data.teamName,
          isActive: data.isActive
      })
    }
  }
  @Output()
  isSuccessChanged = new EventEmitter<boolean>();
  ngOnInit() {
    this.getUsers();
  }
  getUsers() {
    this.authService.getUserDetails().subscribe((response: any) => {
      this.userDetails = response.data;
    })
  }
  setTeamsForm() {
    this.teamsForm = new FormGroup({
      teamId: new FormControl(null),
      teamAdminId: new FormControl('',[Validators.required]),
      description: new FormControl(),
      teamName: new FormControl('',[Validators.required]),
      isActive: new FormControl(true)
    })
  }
  onSubmit() {
    var inputModel = new Teams();
    inputModel.teamId = this.teamsForm.get('teamId').value;
    inputModel.teamAdminId = this.teamsForm.get('teamAdminId').value;
    inputModel.description = this.teamsForm.get('description').value;
    inputModel.teamName = this.teamsForm.get('teamName').value;
    inputModel.isActive = this.teamsForm.get('isActive').value;
    this.teamsService.upsertTeams(inputModel).subscribe((response:any)=>{
     if(response.success){
      this.toaster.success('team added successfully');
      this.closeForm(true);
     }
    })
  }
  closeForm(isSuccess){
    this.isSuccessChanged.emit(isSuccess);
  }
}
