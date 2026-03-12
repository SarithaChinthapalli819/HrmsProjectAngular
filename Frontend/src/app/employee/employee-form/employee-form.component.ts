import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AuthService } from '../../core/auth.serive';
import { FormControl, FormGroup } from '@angular/forms';
import { User } from '../../Models/User.model';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-employee-form',
    templateUrl: './employee-form.component.html',
    standalone: false
})
export class EmployeeFormComponent implements OnInit {
  constructor(private authService: AuthService, private toaster: ToastrService,private cdr : ChangeDetectorRef) {

  }
  isResetPasswordClicked: boolean = false;
  departmentList = [];
  designationList = [];
  rolesList = [];
  statusList = [
    { id: true, name: 'Active' },
    { id: false, name: 'InActive' }
  ]
  @Input()
  set data(data) {
     this.setUserForm();
    if (data) {
      this.userForm.patchValue({
        firstName: data.firstName,
        lastName: data.lastName,
        userName: data.userName,
        email: data.email,
        roleId: data.roleId,
        departmentId: data.departmentId,
        designationId: data.designationId,
        isActive: data.isActive,
        employeeCode: data.employeeCode,
        joiningDate: data.joiningDate ? data.joiningDate.split('T')[0] : '',
        userId : data.userId
      });
      this.cdr.detectChanges();
    }
  }
  userForm: FormGroup;
  @Output() formClosed = new EventEmitter<boolean>();

  closeForm(event) {
    this.formClosed.emit(event);
  }
  ngOnInit(): void {
    this.getDepartments();
    this.getDesignations();
    this.getRoles(); 
  }
  setUserForm() {
    this.userForm = new FormGroup({
      userId: new FormControl(null),
      firstName: new FormControl(''),
      lastName: new FormControl(''),
      userName: new FormControl(''),
      email: new FormControl(''),
      roleId: new FormControl(''),
      departmentId: new FormControl(''),
      designationId: new FormControl(''),
      isActive: new FormControl(''),
      employeeCode: new FormControl(''),
      joiningDate: new FormControl('')
    })
  }

  getDepartments() {
    this.authService.getDepartments().subscribe((response: any) => {
      this.departmentList = response.data;
    });
  }
  getDesignations() {
    this.authService.getDesignations().subscribe((response: any) => {
      this.designationList = response.data;
    });
  }
  getRoles() {
    this.authService.getRoles().subscribe((response: any) => {
      this.rolesList = response.data;
    });
  }
  upsertEmployee() {
    var inputmodel = new User();
    inputmodel.FirstName = this.userForm.get('firstName').value;
    inputmodel.LastName = this.userForm.get('lastName').value;
    inputmodel.UserName = this.userForm.get('userName').value;
    inputmodel.Email = this.userForm.get('email').value;
    inputmodel.JoiningDate = this.userForm.get('joiningDate').value;
    inputmodel.DepartmentId = this.userForm.get('departmentId').value;
    inputmodel.DesignationId = this.userForm.get('designationId').value;
    inputmodel.IsActive = this.userForm.get('isActive').value;
    inputmodel.IsRegister = false;
    inputmodel.Password = 'Test123!';
    inputmodel.RoleId = this.userForm.get('roleId').value;
    inputmodel.EmployeeCode = this.userForm.get('employeeCode').value;
    inputmodel.UserId = this.userForm.get('userId').value;
    this.authService.register(inputmodel).subscribe((response: any) => {
      if (response.success) {
        this.toaster.success('Employee Created Successfully....');
        this.closeForm(true);
      }
    })
  }
}
