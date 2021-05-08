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



@NgModule({
  declarations: [
    AddTenantComponent,
    TenantListComponent,
    TenantCardComponent,
    TenantViewComponent,
    TenantBillComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule,
    BillModule
  ],
  exports:[
    AddTenantComponent,
    TenantViewComponent,
    TenantListComponent
  ]
})
export class TenantModule { }
