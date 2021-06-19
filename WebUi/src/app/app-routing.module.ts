import { ResetPasswordComponent } from './core/reset-password/reset-password.component';
import { ForgotPasswordComponent } from './core/forgot-password/forgot-password.component';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { AdminComponent } from './admin/admin.component';
import { SettingsComponent } from './settings/settings.component';
import { VendorListComponent } from './vendor/vendor-list/vendor-list.component';
import { FeedbackComponent } from './feedback/feedback.component';
import { DashboardComponent } from './home/dashboard/dashboard.component';
import { AuthGuard } from './core/guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VendorDetailComponent } from './vendor/vendor-detail/vendor-detail.component';
import { AdminGuard } from './core/guards/admin.guard';

const routes: Routes = [
  { path: '', component: HomeComponent, data: { breadcrumb: 'Home' } },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'dashboard', component: DashboardComponent },
      { path: 'feedback', component: FeedbackComponent },
      { path: 'settings', component: SettingsComponent },
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
        data: { breadcrumb: 'Accounts' },
      },
      {
        path: 'admin',
        loadChildren:() => import('./admin/admin.module').then((mod) => mod.AdminModule),
        // canActivate:[AdminGuard],
        data: { breadcrumb: 'Admin Panel' },
      }
    ],
  },
  { path: 'not-found', component: NotFoundComponent},
  { path: 'account/forgot-password', component: ForgotPasswordComponent,data : {breadcrumb:'Forgot Password'}},
  { path: 'account/reset-password', component: ResetPasswordComponent,data : {breadcrumb:'Reset Password'}},
  { path: '**', component: NotFoundComponent,pathMatch:'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
