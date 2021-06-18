import { RolesModalComponent } from './../roles-modal/roles-modal.component';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AdminService } from './../admin.service';
import { User } from 'src/app/shared/models/user';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.scss']
})
export class UserManagementComponent implements OnInit {

  @Input() users: Partial<User>[];
  bsModalRef: BsModalRef;

  constructor(private adminService: AdminService , private modalService: BsModalService) { }

  ngOnInit(): void {
    
  }

  

  openRolesModal(user: User){
    const config = {
      class:'modal-dialog-centered',
      initialState: {
        user,
        roles: this.getRolesArray(user)
      }
    };
    this.bsModalRef = this.modalService.show(RolesModalComponent, config);
    this.bsModalRef.content.updateSelectedRoles.subscribe(values => {
      const rolesToUpdate = {
        roles: [...values].filter(el => el.checked === true).map(el => el.name)
      };
      if(rolesToUpdate){
        this.adminService.updateUserRoles(user.userName,rolesToUpdate.roles).subscribe(() => {
          user.roles = [...rolesToUpdate.roles];
        })
      }
    })
  }

  private getRolesArray(user){
    const roles =  [];
    const userRoles = user.roles;
    const availableRoles: any[] = [
      {name:'Admin',value:'Admin'},
      {name:'Accountant',value:'Accountant'},
      {name:'Member',value:'Member'},
    ];

    availableRoles.forEach(role => {
      let isMatch = false;
      for (const userRole of userRoles){
        if(role.name === userRole){
          isMatch = true;
          role.checked = true;
          roles.push(role);
          break;
        }       
      }
      if(!isMatch){
        role.checked = false;
        roles.push(role);
      }
    });
    return roles;
  }

}
