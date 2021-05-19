import { VendorListComponent } from './vendor/vendor-list/vendor-list.component';
import { FeedbackComponent } from './feedback/feedback.component';
import { DashboardComponent } from './home/dashboard/dashboard.component';
import { AuthGuard } from './core/guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VendorDetailComponent } from './vendor/vendor-detail/vendor-detail.component';

const routes: Routes = [
  { path: '', component: HomeComponent, data: { breadcrumb: 'Home' } },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'dashboard', component: DashboardComponent },
      { path: 'feedback', component: FeedbackComponent },
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

      {
        path: 'vendors',
        loadChildren: () =>
          import('./vendor/vendor.module').then((mod) => mod.VendorModule),
        data: { breadcrumb: 'Vendors' },
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
