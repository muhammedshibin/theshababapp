import { BillRoutingModule } from './bill-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GenerateBillComponent } from './generate-bill/generate-bill.component';
import { SharedModule } from '../shared/shared.module';
import { BillListComponent } from './bill-list/bill-list.component';



@NgModule({
  declarations: [
    GenerateBillComponent,
    BillListComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports:[BillRoutingModule]
})
export class BillModule { }
