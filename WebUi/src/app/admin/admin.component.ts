import { AdminService } from './admin.service';
import { Component, OnInit } from '@angular/core';
import { TabDirective } from 'ngx-bootstrap/tabs';
import { User } from '../shared/models/user';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

  users: Partial<User>[];

  constructor(private adminService: AdminService) { }

  ngOnInit(): void {
    this.loadUsersWithRoles();
  }

  onSelect(tab: TabDirective) {
    if (tab.heading === 'User Management') {
      this.loadUsersWithRoles();
      //this.loadTenant();
    }
    
  }  
  

  loadUsersWithRoles(){
    this.adminService.getUsersWithRoles().subscribe(users => {
      this.users = users;
    })
  }

}
