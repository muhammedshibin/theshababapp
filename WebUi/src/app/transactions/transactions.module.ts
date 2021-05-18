import { TransactionsRoutingModule } from './transactions-routing.module';
import { SharedModule } from './../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddTransactionComponent } from './add-transaction/add-transaction.component';
import { TransactionsListComponent } from './transactions-list/transactions-list.component';



@NgModule({
  declarations: [
    AddTransactionComponent,
    TransactionsListComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports:[TransactionsRoutingModule]
})
export class TransactionsModule { }
