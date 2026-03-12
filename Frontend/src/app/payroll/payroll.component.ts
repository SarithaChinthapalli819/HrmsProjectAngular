import { Component, OnInit } from '@angular/core';
import { PayrollService } from '../core/payroll.service';

@Component({
  selector: 'app-payroll',
  templateUrl: './payroll.component.html'
})
export class PayrollComponent implements OnInit{
  constructor(private payrollService : PayrollService){

  }
  payrollData = [];
  ngOnInit(): void {
    this.getPayrollData();
  }
  getPayrollData(){
    this.payrollService.getPayroll().subscribe((response:any)=>{
      var response = response.data;
      this.payrollData = response;
    })
  }
  isPayrollGenerateOpened : boolean = false;
  openPayrollGenerate(){
    this.isPayrollGenerateOpened = true;
  }
  isCanceled(event){
    this.isPayrollGenerateOpened = false;
  }
}
