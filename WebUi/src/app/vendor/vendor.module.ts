import { VendorRoutingModule } from './vendor-routing.module';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VendorListComponent } from './vendor-list/vendor-list.component';
import { VendorDetailComponent } from './vendor-detail/vendor-detail.component';



@NgModule({
  declarations: [
    VendorListComponent,
    VendorDetailComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports:[VendorRoutingModule]
})
export class VendorModule { }
