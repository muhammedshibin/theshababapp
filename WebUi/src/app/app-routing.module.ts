import { TransactionsListComponent } from './transactions/transactions-list/transactions-list.component';
import { AddTransactionComponent } from './transactions/add-transaction/add-transaction.component';
import { AddTenantComponent } from './tenant/add-tenant/add-tenant.component';
import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TenantListComponent } from './tenant/tenant-list/tenant-list.component';

const routes: Routes = [
  {path:'',component:HomeComponent},
  {path:'tenants/registration', component:AddTenantComponent},
  {path:'transactions/add-transaction', component:AddTransactionComponent},
  {path:'transactions/list', component:TransactionsListComponent},
  {path:'transactions/:id', component:AddTransactionComponent},
  {path:'tenants/list', component:TenantListComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
