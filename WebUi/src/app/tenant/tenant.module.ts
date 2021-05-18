import { TenantRoutingModule } from './tenant-routing.module';
import { BillModule } from './../bill/bill.module';
import { RouterModule } from '@angular/router';
import { SharedModule } from './../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddTenantComponent } from './add-tenant/add-tenant.component';
import { TenantListComponent } from './tenant-list/tenant-list.component';
import { TenantCardComponent } from './tenant-card/tenant-card.component';
import { TenantViewComponent } from './tenant-view/tenant-view.component';
import { TenantBillComponent } from './tenant-bill/tenant-bill.component';
import { TenantLeaveComponent } from './tenant-leave/tenant-leave.component';
import { ApplyLeaveComponent } from './apply-leave/apply-leave.component';



@NgModule({
  declarations: [
    AddTenantComponent,
    TenantListComponent,
    TenantCardComponent,
    TenantViewComponent,
    TenantBillComponent,
    TenantLeaveComponent,
    ApplyLeaveComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule
  ],
  exports:[
    TenantRoutingModule
  ]
})
export class TenantModule { }
