import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { Component, Input, OnInit, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-date-input',
  templateUrl: './date-input.component.html',
  styleUrls: ['./date-input.component.scss']
})
export class DateInputComponent implements ControlValueAccessor {

  @Input() label : string;
  @Input() inputDate: Date;
  bsConfig: Partial<BsDatepickerConfig>;
  bsValue = new Date();

  constructor(@Self() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this;
    this.bsConfig = {
      //dateInputFormat: 'DD MMMM YYYY'
      dateInputFormat: 'YYYY-MM-DD'
    }
    this.bsValue = this.inputDate || new Date();

   }
  writeValue(obj: any): void {
  }
  registerOnChange(fn: any): void {
  }
  registerOnTouched(fn: any): void {
  }
  setDisabledState?(isDisabled: boolean): void {
   
  } 
}
