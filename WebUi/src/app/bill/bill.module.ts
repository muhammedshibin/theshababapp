import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GenerateBillComponent } from './generate-bill/generate-bill.component';
import { SharedModule } from '../shared/shared.module';
import { BillPaymentComponent } from './bill-payment/bill-payment.component';
import { BillListComponent } from './bill-list/bill-list.component';



@NgModule({
  declarations: [
    GenerateBillComponent,
    BillPaymentComponent,
    BillListComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports:[GenerateBillComponent,BillPaymentComponent]
})
export class BillModule { }
