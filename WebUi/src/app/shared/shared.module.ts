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

@NgModule({
  declarations: [TextInputComponent, DateInputComponent, ModalLgComponent, AlertWindowComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    CarouselModule.forRoot(),
    BsDatepickerModule.forRoot(),
    PaginationModule.forRoot(),
    ModalModule.forRoot(),
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
    AlertWindowComponent
  ],
})
export class SharedModule {}
