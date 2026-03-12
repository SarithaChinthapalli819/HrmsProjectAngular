import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { User } from '../../Models/User.model';
import { AuthService } from '../../core/auth.serive';
import { Router } from '@angular/router';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    standalone: false
})
export class LoginComponent {
  constructor(private authService:AuthService,private router:Router){

  }
  loginCred = new FormGroup({
    userName: new FormControl('',[Validators.required]),
    password : new FormControl('',[Validators.required,Validators.pattern(
    /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+{}\[\]:;"'<>,.?/~`-]).{8,}$/
  )])
  })

  login(){
    var inputModel = new User();
    inputModel.UserName = this.loginCred.get('userName').value;
    inputModel.Password = this.loginCred.get('password').value;
    this.authService.login(inputModel).subscribe((response:any)=>{
      if(response.success){
        localStorage.setItem("authToken",response.data.token);
        localStorage.setItem("userId",response.data.userId)
        this.router.navigate(['/dashboard'])
      }
    })
  }
}
