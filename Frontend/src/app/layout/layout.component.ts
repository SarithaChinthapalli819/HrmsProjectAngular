import { Component } from '@angular/core';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html'
})
export class LayoutComponent {
  menuItems = [
      { label: 'Dashboard', path: '/dashboard', icon: 'fa fa-chart-pie' },
      { label: 'Attendance', path: '/attendance', icon: 'fa fa-clock' },
      { label: 'Leaves', path: '/leaves', icon: 'fa fa-calendar-minus' },
      { label: 'Employees', path: '/employees', icon: 'fa fa-users' },
      { label: 'Teams', path: '/teams', icon: 'fa fa-user-group' },
      { label: 'Projects', path: '/projects', icon: 'fa fa-briefcase' },
      { label: 'Tasks', path: '/tasks', icon: 'fa fa-list-check' },
      { label: 'Timesheets', path: '/timesheets', icon: 'fa fa-calendar-days' },
      { label: 'Payroll', path: '/payroll', icon: 'fa fa-money-bill' }
  ]
}
