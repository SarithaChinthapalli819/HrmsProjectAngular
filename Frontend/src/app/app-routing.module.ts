import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';

const routes: Routes = [
  {
    path: '', component: LayoutComponent, children: [
      {
        path:'',redirectTo:'dashboard',pathMatch:'full'
      },
      {
        path: 'dashboard', loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule)
      },
      {
        path: 'attendance', loadChildren: () => import('./attendance/attendance.module').then(m => m.Attendancemodule)
      },
      {
        path: 'employees', loadChildren: () => import('./employee/employee.module').then(m => m.Emmployemodule)
      },
      {
        path: 'leaves', loadChildren: () => import('./leaves/leaves.module').then(m => m.Leavesmodule)
      },
      {
        path: 'payroll', loadChildren: () => import('./payroll/payroll.module').then(m => m.PayrollModule)
      },
      {
        path: 'teams', loadChildren: () => import('./teams/teams.module').then(m => m.Teamsmodule)
      },
      {
        path: 'timesheets', loadChildren: () => import('./timesheet/timesheet.module').then(m => m.Timesheetmodule)
      },
      {
        path: 'projects', loadChildren: () => import('./projects/projects.module').then(m => m.Projectmodule)
      },
      {
        path: 'tasks', loadChildren: () => import('./tasks/tasks.module').then(m => m.Tasksmodule)
      }

    ]
  },
   {
    path:'auth',loadChildren:()=>import('./auth/auth.module').then(m=>m.AuthModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
