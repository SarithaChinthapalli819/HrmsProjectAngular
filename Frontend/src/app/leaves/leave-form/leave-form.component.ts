import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { LeaveService } from '../../core/leaves.service';
import { AuthService } from '../../core/auth.serive';
import { FormControl, FormGroup } from '@angular/forms';
import { Leaves } from '../../Models/leaves.model';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-leave-form',
    templateUrl: './leave-form.component.html',
    standalone: false
})
export class LeaveFormComponent implements OnInit {
  leaveTypes = [];
  userDetails = [];
  leavesForm : FormGroup;
  @Output()
  popupClosed = new EventEmitter<boolean>();
  constructor(private authService : AuthService,private leavesService:LeaveService,private toaster:ToastrService){

  }
  @Input()
  set data(data) {
    this.setLeavesForm();
    if(data){
      this.leavesForm.patchValue({
        leaveId : data.leaveId,
        leaveTypeId : data.leaveTypeId,
        userId : data.userId,
        dateFrom : data.dateFrom ? data.dateFrom.split('T')[0] : '',
        dateTo : data.dateTo ? data.dateTo.split('T')[0] : '', 
        reason  : data.description
      })
    }
  }
  ngOnInit() {
    this.getLeaveTypes();
    this.getUserDetails();
  }
  getLeaveTypes(){
    this.leavesService.getLeaveTypes().subscribe((response:any)=>{
      if(response && response.success){
        this.leaveTypes = response.data;
      }
    })
  }
  getUserDetails(){
    this.authService.getUserDetails().subscribe((response:any)=>{
      this.userDetails = response.data;
    })
  }
  closeForm(event){
    this.popupClosed.emit(event)
  }
  setLeavesForm(){
    this.leavesForm = new FormGroup({
      leaveId : new FormControl(''),
      leaveTypeId : new FormControl(''),
      userId : new FormControl(''),
      dateFrom : new FormControl(''),
      dateTo : new FormControl(''),
      reason : new FormControl('')
    });
  }
  upsertLeave(){
    var leavesmodel = new Leaves();
    leavesmodel.LeaveId = this.leavesForm.get('leaveId').value;
    leavesmodel.UserId = this.leavesForm.get('userId').value;
    leavesmodel.LeaveTypeId = this.leavesForm.get('leaveTypeId').value;
    leavesmodel.DateFrom = this.leavesForm.get('dateFrom').value;
    leavesmodel.DateTo = this.leavesForm.get('dateTo').value;
    leavesmodel.Description = this.leavesForm.get('reason').value;
    leavesmodel.isApproved = null;
    this.leavesService.upsertLeaves(leavesmodel).subscribe((response:any)=>{
      if(response.success){
        this.toaster.success('Leave Applied Successfully...');
        this.closeForm(true);
      }
      else{
        this.toaster.error(response.apiResponseMessages[0].message)
      }
    })
  }
}
