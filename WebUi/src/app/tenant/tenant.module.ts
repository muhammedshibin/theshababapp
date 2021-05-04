import { SharedModule } from './../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddTenantComponent } from './add-tenant/add-tenant.component';
import { TenantListComponent } from './tenant-list/tenant-list.component';
import { TenantCardComponent } from './tenant-card/tenant-card.component';



@NgModule({
  declarations: [
    AddTenantComponent,
    TenantListComponent,
    TenantCardComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports:[
    AddTenantComponent
  ]
})
export class TenantModule { }
