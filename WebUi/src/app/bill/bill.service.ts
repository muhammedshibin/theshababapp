import { InmateBill } from './../shared/models/inmateBill';
import { BillParams } from './../shared/models/billParams';
import { MonthlyBill } from './../shared/models/bill';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import {
  getPaginatedResult,
  getPaginationHeaders,
} from '../shared/functions/pagination_functions';

@Injectable({
  providedIn: 'root',
})
export class BillService {
  baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) {}

  generateMonthlyBills(month: number, year: number) {
    return this.http.get<MonthlyBill>(
      this.baseUrl + `Bills/total/${month}/${year}`
    );
  }

  generateIndividualBills(month: number, year: number) {
    return this.http.post(this.baseUrl + `Bills/${month}/${year}`, {});
  }

  payBill(payment: any) {
    return this.http.post(this.baseUrl + 'Bills/payment', payment);
  }
  getBills(billParams: BillParams) {
    let params = getPaginationHeaders(
      billParams.pageNumber,
      billParams.pageSize
    );
    if (billParams.month) {
      params = params.append('month', billParams.month.toString());
    }
    if (billParams.year) {
      params = params.append('year', billParams.year.toString());
    }
    if (billParams.search) {
      params = params.append('month', billParams.search);
    }
    if (billParams.paidPredicate) {
      params = params.append('month', billParams.paidPredicate);
    }

    return getPaginatedResult<InmateBill>(
      this.http,
      this.baseUrl + 'bills',
      params
    );
  }
}
