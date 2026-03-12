import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../core/auth.serive';
import { User } from '../../Models/User.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent {

  constructor(private authService : AuthService){

  }
  registerForm = new FormGroup({
    firstName : new FormControl('',[Validators.required]),
    lastName : new FormControl('',[Validators.required]),
    userName : new FormControl('',[Validators.required]),
    email : new FormControl('',[Validators.required,Validators.email]),
    password : new FormControl('',[Validators.required, Validators.pattern(
    /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+{}\[\]:;"'<>,.?/~`-]).{8,}$/
  )])
  })

  register(){
    var inputModel = new User();
    inputModel.UserId = null;
    inputModel.FirstName  = this.registerForm.get('firstName').value;
    inputModel.LastName  = this.registerForm.get('lastName').value;
    inputModel.UserName  = this.registerForm.get('userName').value;
    inputModel.Email  = this.registerForm.get('email').value;
    inputModel.Password  = this.registerForm.get('password').value;
    this.authService.register(inputModel).subscribe((response)=>{
      this.registerForm.reset();
    })
  }

  firstnamechange(){
    console.log(this.registerForm)
  }
}
