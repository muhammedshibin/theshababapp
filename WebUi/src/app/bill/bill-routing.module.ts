import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GenerateBillComponent } from './generate-bill/generate-bill.component';
import { BillListComponent } from './bill-list/bill-list.component';

const routes = [
  {
    path: '',
    component: BillListComponent,
    data: { breadcrumb: 'Bills' },
  },
  {
    path: 'add-bill',
    component: GenerateBillComponent,
    data: {
      breadcrumb: 'Monthly Bill Generation',
    },
  },
  {
    path: 'list',
    component: BillListComponent,
    data: { breadcrumb: 'Bills' },
  },
];

@NgModule({
  declarations: [],
  imports: [CommonModule , RouterModule.forChild(routes)],
  exports:[RouterModule]
})
export class BillRoutingModule {}
