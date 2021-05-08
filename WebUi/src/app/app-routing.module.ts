import { GenerateBillComponent } from './bill/generate-bill/generate-bill.component';
import { TenantViewComponent } from './tenant/tenant-view/tenant-view.component';
import { TransactionsListComponent } from './transactions/transactions-list/transactions-list.component';
import { AddTransactionComponent } from './transactions/add-transaction/add-transaction.component';
import { AddTenantComponent } from './tenant/add-tenant/add-tenant.component';
import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TenantListComponent } from './tenant/tenant-list/tenant-list.component';
import { BillListComponent } from './bill/bill-list/bill-list.component';

const routes: Routes = [
  {path:'',component:HomeComponent},
  {path:'bills/add-bill',component:GenerateBillComponent},
  {path:'bills/list',component:BillListComponent},
  {path:'transactions/add-transaction', component:AddTransactionComponent},
  {path:'transactions/list', component:TransactionsListComponent},
  {path:'transactions/:id', component:AddTransactionComponent},  
  {path:'tenants/add-tenant', component:AddTenantComponent},
  {path:'tenants/list', component:TenantListComponent},
  {path:'tenants/:id', component:TenantViewComponent}
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
