import { AddTenantComponent } from './tenant/add-tenant/add-tenant.component';
import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TenantListComponent } from './tenant/tenant-list/tenant-list.component';

const routes: Routes = [
  {path:'',component:HomeComponent},
  {path:'tenants/registration', component:AddTenantComponent},
  {path:'tenants/list', component:TenantListComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
