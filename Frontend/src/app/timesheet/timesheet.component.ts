import { Component, OnInit } from '@angular/core';
import { TimesheetService } from '../core/timesheet.service';

@Component({
    selector: 'app-timesheet',
    templateUrl: './timesheet.component.html',
    standalone: false
})
export class TimesheetComponent implements OnInit {
  constructor(private timesheetService: TimesheetService) {

  }
  ngOnInit(): void {
    this.getTimesheetData();
    this.getTimesheetDetailsData();
  }
  timesheetData = [];
  isDetailsOpened: boolean = false;
  selectedMonth: Date = new Date();
  timesheetDetailsData = [];

  changeMonth(offset: number) {
    this.selectedMonth = new Date(
      this.selectedMonth.getFullYear(),
      this.selectedMonth.getMonth() + offset,
      1
    );
    this.getTimesheetData();
  }
  getTimesheetData() {
    var model = {
      //userId: localStorage.getItem("userId"),
      month: this.selectedMonth.getMonth() + 1,
      year: this.selectedMonth.getFullYear(),
      userId: null
    };
    this.timesheetService.getTimesheetData(model).subscribe((response: any) => {
      if (response.success) {
        this.timesheetData = response.data;
      }
    })
  }

  getTimesheetDetailsData() {
    var model = {
      //userId: localStorage.getItem("userId"),
      month: this.selectedMonth.getMonth() + 1,
      year: this.selectedMonth.getFullYear(),
      userId: null
    };
    this.timesheetService.getTimesheetDetailsData(model).subscribe((response: any) => {
      if (response.success) {
        var dataSet = response.data;
        dataSet.forEach((data) => {
          var item = this.timesheetDetailsData.find(item => item.key === data.date.split('T')[0]);
          if (!item) {
            this.timesheetDetailsData.push({
              key: data.date.split('T')[0],
              values: [{...data}]
            })
          }
          else {
            item.values.push(data)
          }
        })
        console.log(this.timesheetDetailsData)
      }
    })
  }
}
