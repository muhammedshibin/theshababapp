import { DashboardComponent } from './home/dashboard/dashboard.component';
import { AuthGuard } from './core/guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', component: HomeComponent, data: { breadcrumb: 'Home' } },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'dashboard', component: DashboardComponent },
      {
        path: 'bills',
        loadChildren: () =>
          import('./bill/bill.module').then((mod) => mod.BillModule),
        data: { breadcrumb: 'Bills' },
      },
      {
        path: 'transactions',
        loadChildren: () =>
          import('./transactions/transactions.module').then(
            (mod) => mod.TransactionsModule
          ),
        data: { breadcrumb: 'Transactions' },
      },
      {
        path: 'tenants',
        loadChildren: () =>
          import('./tenant/tenant.module').then((mod) => mod.TenantModule),
        data: { breadcrumb: 'Inmates' },
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
