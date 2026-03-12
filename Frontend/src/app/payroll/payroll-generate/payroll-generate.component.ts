import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AuthService } from '../../core/auth.serive';
import { Form, FormArray, FormControl, FormGroup } from '@angular/forms';
import { PayrollModel } from '../../Models/PayrollModel';
import { PayrollService } from '../../core/payroll.service';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-payroll-generate',
    templateUrl: './payroll-generate.component.html',
    standalone: false
})
export class PayrollGenerateComponent implements OnInit {
  userDetails = [];
  payrollForm: FormGroup;
  isSelectedAll: boolean = false;
  @Output()
  isCanceled = new EventEmitter<boolean>();
  constructor(private authService: AuthService,private payrollService : PayrollService,private toaster:ToastrService) {

  }
  ngOnInit() {
    this.getUserDetails();
    this.setForm();
  }

  getUserDetails() {
    this.authService.getUserDetails().subscribe((response: any) => {
      if (response && response.success) {
        this.userDetails = response.data;
        this.createEmployeeForm(this.userDetails);
      }
    })
  }
  setForm() {
    this.payrollForm = new FormGroup({
      payrollId:new FormControl(null),
      month: new FormControl(1),
      year: new FormControl(2026),
      userDetails: new FormArray([])
    });
  }
  get UserDetails(): FormArray {
    return this.payrollForm.get('userDetails') as FormArray;
  }

  createEmployeeForm(userDetails) {
    userDetails.forEach((item: any) => {
      this.UserDetails.push(new FormGroup({
        employeeId: new FormControl(item.employeeId),
        id:new FormControl(null), 
        userName: new FormControl(item.userName),
        basicSalary: new FormControl(50000),
        allowance: new FormControl(15000),
        deduction: new FormControl(5000),
        netSalary: new FormControl(60000),
        isEdit: new FormControl(false),
        isSelected: new FormControl(false)
      }))
    })
  }
  isSelectedAllClicked() {
    this.isSelectedAll = !this.isSelectedAll;
    this.UserDetails.controls.forEach((item: any) => {
      item.get('isSelected').setValue(this.isSelectedAll);
    })
  }
  isItemClicked(i) {
    var formGroup = (this.payrollForm.get('userDetails') as FormArray).at(i) as FormGroup;
    var isSelected = formGroup.get('isSelected').value;  
    formGroup.get('isSelected').setValue(!isSelected);
  }

  onCancel(event){
    this.isCanceled.emit(event);
  }
  upsertPayroll(){
    var model = new PayrollModel();
    model.PayrollId = this.payrollForm.get('payrollId').value;
    model.Month = this.payrollForm.get('month').value;
    model.Year = this.payrollForm.get('year').value;
    var salaryList = [];
    this.UserDetails.controls.forEach(item=>{
      if(item.get('isSelected').value){
         salaryList.push({
          Id:item.get('id').value,
          EmployeeId:item.get('employeeId').value,
          BasicSalary:item.get('basicSalary').value,
          Allowances:item.get('allowance').value,
          Deductions:item.get('deduction').value,
          NetSalary:item.get('netSalary').value
      })
      }
    });
    model.SalaryJson = JSON.stringify(salaryList);
    this.payrollService.upsertPayroll(model).subscribe((response:any)=>{
      if(response.success){
        this.toaster.success("payroll updated succesfully...");
      }
    })
  }

  onChange(item){
    item.get('netSalary').setValue((item.get('basicSalary').value + item.get('allowance').value) - item.get('deduction').value)
  }
}

