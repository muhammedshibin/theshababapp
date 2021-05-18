import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransactionsListComponent } from './transactions-list/transactions-list.component';
import { AddTransactionComponent } from './add-transaction/add-transaction.component';

const routes = [
  {
    path: '',
    component: TransactionsListComponent,
    data: { breadcrumb: 'Transactions' },
  },
  {
    path: 'add-transaction',
    component: AddTransactionComponent,
    data: { breadcrumb: 'Add Transaction' },
  },
  {
    path: 'list',
    component: TransactionsListComponent,
    data: { breadcrumb: 'Transactions' },
  },
  {
    path: ':id',
    component: AddTransactionComponent,
    data: { breadcrumb: 'Edit Transaction' },
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes), CommonModule],
  exports: [RouterModule],
})
export class TransactionsRoutingModule {}
