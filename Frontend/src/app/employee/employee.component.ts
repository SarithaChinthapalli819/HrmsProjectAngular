import { Component, Input, OnInit } from '@angular/core';
import { AuthService } from '../core/auth.serive';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html'
})
export class EmployeeComponent implements OnInit{

  userDetails = [];
  data:any;
  constructor(private authService : AuthService){

  }
  ngOnInit(): void {
     this.getUserDetails();
  }
  getUserDetails(){
    this.authService.getUserDetails().subscribe((response:any)=>{
      this.userDetails = response.data;
    })
  }
  isOpened : boolean =false; 
  openEmployeeForm(){
    this.data = null;
    this.isOpened = true;
  }
  formClosed(event){
    this.isOpened = false;
    if(event){
      this.getUserDetails();
    }
  }
  editEmployee(item){
    this.data = item;
    this.isOpened = true;
  }
}
