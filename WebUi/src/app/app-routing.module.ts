import { DashboardComponent } from './home/dashboard/dashboard.component';
import { AuthGuard } from './core/guards/auth.guard';
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
  { path: '', component: HomeComponent, data: { breadcrumb: 'Home' } },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'dashboard', component: DashboardComponent},
      { path: 'bills/add-bill', component: GenerateBillComponent },
      {
        path: 'bills/list',
        component: BillListComponent,
        data: { breadcrumb: 'Bills' },
      },
      {
        path: 'transactions/add-transaction',
        component: AddTransactionComponent,
        data: { breadcrumb: 'Add Transaction' },
      },
      {
        path: 'transactions/list',
        component: TransactionsListComponent,
        data: { breadcrumb: 'Transactions' },
      },
      {
        path: 'transactions/:id',
        component: AddTransactionComponent,
        data: { breadcrumb: 'Edit Transaction' },
      },
      { path: 'tenants/add-tenant', component: AddTenantComponent },
      {
        path: 'tenants/list',
        component: TenantListComponent,
        data: { breadcrumb: 'Inmates' },
      },
      {
        path: 'tenants/:id',
        component: TenantViewComponent,
        data: { breadcrumb: { alias: 'inmateName' } },
      },
    ],
  },  
  { path: '**', component: HomeComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
