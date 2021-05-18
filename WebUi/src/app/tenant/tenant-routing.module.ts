import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddTenantComponent } from './add-tenant/add-tenant.component';
import { TenantListComponent } from './tenant-list/tenant-list.component';
import { TenantViewComponent } from './tenant-view/tenant-view.component';

const routes = [
  {
    path: '',
    component: TenantListComponent,
    data: { breadcrumb: 'Inmates' },
  },
  {
    path: 'add-tenant',
    component: AddTenantComponent,
    data: { breadcrumb: 'Add Inmate' },
  },
  {
    path: 'list',
    component: TenantListComponent,
    data: { breadcrumb: 'Inmates' },
  },
  {
    path: ':id',
    component: TenantViewComponent,
    data: { breadcrumb: { alias: 'inmateName' } },
  },
];

@NgModule({
  declarations: [],
  imports: [CommonModule , RouterModule.forChild(routes)],
  exports:[RouterModule]
})
export class TenantRoutingModule {}
