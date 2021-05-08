import { MonthlyBill } from './../../shared/models/bill';
import { BillService } from './../bill.service';
import {
  FormStyle,
  getLocaleMonthNames,
  TranslationWidth,
} from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { getMonth } from 'ngx-bootstrap/chronos';
import { DATE } from 'ngx-bootstrap/chronos/units/constants';
import { getDate, getFullYear } from 'ngx-bootstrap/chronos/utils/date-getters';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-generate-bill',
  templateUrl: './generate-bill.component.html',
  styleUrls: ['./generate-bill.component.scss'],
})
export class GenerateBillComponent implements OnInit {
  monthNames: readonly string[] = ["January", "February", "March", "April", "May", "June",
  "July", "August", "September", "October", "November", "December"
];
  selectedMonth = new Date().getUTCMonth();
  selectedYear = new Date().getUTCFullYear();
  monthlyBill: MonthlyBill;
  currentMonthName: string;

  constructor(private billService: BillService , private toastr: ToastrService) {}

  ngOnInit(): void {    
  }

  onSubmit() {
    this.billService
      .generateMonthlyBills(this.selectedMonth, this.selectedYear)
      .subscribe(
        (data) => {
          this.monthlyBill = data;
        },
        (err) => {}
      );
  }

  onProceeding(){
    this.billService.generateIndividualBills(this.selectedMonth,this.selectedYear).subscribe(
      (resp) => {
        this.toastr.success('Bills Generated Succesfully','Success');
      },err => {
        this.toastr.error('Some Error occured' , 'Error');
      }
    )
  }

  getMonthName(num: number){
    return this.monthNames[num];
  }
}
