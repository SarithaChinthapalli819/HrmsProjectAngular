import { Component, OnInit } from '@angular/core';
import { AttendanceService } from '../core/attendance.service';
import { AttendanceModel } from '../Models/attendance.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-attendance',
  templateUrl: './attendance.component.html'
})
export class AttendanceComponent implements OnInit{
  constructor(private attendanceService: AttendanceService,private toast:ToastrService) {

  }
  ngOnInit(): void {
    this.dateFrom = new Date().toISOString().split('T')[0];
    this.dateTo = new Date().toISOString().split('T')[0];
    this.getAttendanceData();
    setInterval(() => {
          this.currentTime = new Date();
          this.updateDisplayTime();
      }, 1000);
  }
  attendanceData = [];
  dateFrom :string;
  dateTo : string;
  displayTime:string;
  checkIn : string;
  currentTime = new Date();
  workingDays : any;
  presentDays: any;
  totalHours: any;
  getAttendanceData(){
    var model = new AttendanceModel();
    model.userId = localStorage.getItem("userId");
    model.dateFrom = this.dateFrom;
    model.dateTo = this.dateTo;
    this.attendanceService.getAttendanceData(model).subscribe((response:any)=>{
      this.attendanceData = response.data;
      this.isCheckInClicked = this.attendanceData[0]?.isCheckedIn;
      this.workingDays = this.attendanceData[0]?.workingDays;
       this.presentDays = this.attendanceData[0]?.presentDays;
        this.totalHours = this.attendanceData[0]?.totalHours;
      this.checkIn = this.attendanceData.find(item=>item.checkIn != null && item.checkOut == null)?.checkInTime; 
    })
  }

  isCheckInClicked: boolean = false;
  onCheckIn() {
    this.isCheckInClicked = true;
    this.upsertAttendance();
  }
  onCheckOut() {
    this.isCheckInClicked = false;
    this.upsertAttendance();
  }
  upsertAttendance() {
    var model = new AttendanceModel();
    if (this.isCheckInClicked) {
      model.checkInTime = new Date();
      model.checkOutTime = null;
    }
    else {
      model.checkInTime = null;
      model.checkOutTime = new Date();
    }
    model.userId = localStorage.getItem("userId"); 
    this.attendanceService.upsertAttendance(model).subscribe((response:any)=>{
      if(response.success){
        this.toast.success("Attendance Updated Successfully...");
        this.getAttendanceData();
      }
    })

  }


   updateDisplayTime() {
        if (this.isCheckInClicked) {
            const checkIn = new Date(this.checkIn); 
            const diff = this.currentTime.getTime() - checkIn.getTime();
            if (diff > 0) {
                const hours = Math.floor(diff / 3600000);
                const minutes = Math.floor((diff % 3600000) / 60000);
                const seconds = Math.floor((diff % 60000) / 1000);
                this.displayTime = `${this.pad(hours)}:${this.pad(minutes)}:${this.pad(seconds)}`;
            } else {
                this.displayTime = '00:00:00';
            }
        }  
        else{
          this.displayTime = '00:00:00';
        }
    }

    private pad(num: number): string {
        return num < 10 ? '0' + num : num.toString();
    }
}
