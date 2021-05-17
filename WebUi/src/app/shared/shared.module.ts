import { RouterModule } from '@angular/router';
import { Inmate } from './models/inmate';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { TextInputComponent } from './components/text-input/text-input.component';
import { DateInputComponent } from './components/date-input/date-input.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ModalLgComponent } from './components/modal-lg/modal-lg.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ToastrModule } from 'ngx-toastr';
import { AlertWindowComponent } from './components/alert-window/alert-window.component';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { AccordionModule } from 'ngx-bootstrap/accordion';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { DashboardIconComponent } from './components/dashboard-icon/dashboard-icon.component';

@NgModule({
  declarations: [TextInputComponent, DateInputComponent, ModalLgComponent, AlertWindowComponent, DashboardIconComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    CarouselModule.forRoot(),
    BsDatepickerModule.forRoot(),
    PaginationModule.forRoot(),
    ModalModule.forRoot(),
    TabsModule.forRoot(),
    RouterModule,
    AccordionModule.forRoot(),
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
    }),
  ],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    CarouselModule,
    TextInputComponent,
    DateInputComponent,
    ModalLgComponent,
    BsDatepickerModule,
    PaginationModule,
    ModalModule,
    ToastrModule,
    AlertWindowComponent,
    TabsModule,
    AccordionModule,
    BsDropdownModule,
    DashboardIconComponent
  ],
})
export class SharedModule {}
