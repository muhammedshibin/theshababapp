import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TenantRegistrationComponent } from './tenant-registration/tenant-registration.component';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [TenantRegistrationComponent],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [TenantRegistrationComponent]
})
export class TenantModule { }
