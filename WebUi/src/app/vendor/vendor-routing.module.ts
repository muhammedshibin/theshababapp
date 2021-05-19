import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VendorListComponent } from './vendor-list/vendor-list.component';
import { VendorDetailComponent } from './vendor-detail/vendor-detail.component';

const routes=[
  { path: '', component: VendorListComponent, data: { breadcrumb: 'Vendors' } },
  { path: ':id', component: VendorDetailComponent, data: { breadcrumb: { alias: 'vendorName' } } }
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes),
    CommonModule
  ],
  exports:[RouterModule]
})
export class VendorRoutingModule { }
