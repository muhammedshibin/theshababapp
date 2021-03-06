import { SharedModule } from './../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin.component';
import { UserManagementComponent } from './user-management/user-management.component';
import { RolesModalComponent } from './roles-modal/roles-modal.component';
import { AddUserComponent } from './add-user/add-user.component';
import { AdminRoutingModule } from './admin-routing.module';



@NgModule({
  declarations: [
    AdminComponent,
    UserManagementComponent,
    RolesModalComponent,
    AddUserComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports:[AdminRoutingModule]
})
export class AdminModule { }
