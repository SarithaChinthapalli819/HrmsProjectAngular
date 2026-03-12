import { Component, OnInit } from '@angular/core';
import { LeaveService } from '../core/leaves.service';
import { Leaves } from '../Models/leaves.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-leaves',
  templateUrl: './leaves.component.html'
})
export class LeavesComponent implements OnInit{
  selectedTab = "overview";
  leaveTypes = [];
  isApplyLeave : boolean = false;
  leaves = [];
  data:any;
  constructor(private leavesService:LeaveService,private toast: ToastrService){

  }
  ngOnInit() {
    this.getLeaveTypes();
    this.getLeaves();
  }
  getLeaveTypes(){
    this.leavesService.getLeaveTypes().subscribe((response:any)=>{
      if(response && response.success){
        this.leaveTypes = response.data;
        console.log(this.leaveTypes)
      }
    })
  }
  getStylesOfIcon(item){
    return `text-${item.colour}-600`;
  }
  getBgOfIcon(item){
    return `bg-${item.colour}-50` 
  }
  popupClosed(event){
     this.isApplyLeave = false;
     if(event){
      this.getLeaves();
     }
  }
  getLeaves(){
    this.leavesService.getLeaves().subscribe((response:any)=>{
      this.leaves = response.data;
    })
  }
  getDateDifference(from: any, to: any): number {
    const start = new Date(from).toDateString();
    const end = new Date(to).toDateString();
    const startDate = new Date(start);
    const endDate = new Date(end);
    return (endDate.getTime() - startDate.getTime()) / (1000 * 60 * 60 * 24);
  }
  openApplyLeave(data){
    this.isApplyLeave = true;
    this.data = data;
  }
  approveLeave(){

  }
  deleteLeave(item){
    var leavemodel = new Leaves();
    leavemodel.isArcheive = true;
    leavemodel.LeaveId = item.leaveId;
    this.leavesService.upsertLeaves(leavemodel).subscribe((response:any)=>{
      if(response && response.success){
        this.toast.success('Leave deleted successfully..')
        this.getLeaves();
      }
    })
  }
  approveRequest(item,event){
    var leavemodel = new Leaves();
    leavemodel.isApproved = event;
    leavemodel.LeaveId = item.leaveId;
    leavemodel.approveRequest = true;
    leavemodel.LeaveTypeId = item.leaveTypeId;
    this.leavesService.upsertLeaves(leavemodel).subscribe((response:any)=>{
      if(response && response.success){
        if(event){
          this.toast.success('LeaveApproved Successfully ..')
        }
        else{
        this.toast.success('LeaveRejected Succesfully..');
        }
        this.getLeaves();
      }
    })
  }
}
