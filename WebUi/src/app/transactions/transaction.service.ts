import { TransactionParams } from './../shared/models/transactionParams';
import { Transaction } from './../shared/models/transaction';
import { Category } from './../shared/models/category';
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
export class TransactionService {
  baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) {}

  getTransactionCategories() {
    return this.http.get<Category[]>(this.baseUrl + 'transactions/categories');
  }

  getTransactionById(id: string) {
    return this.http.get<Transaction>(this.baseUrl + 'transactions/' + id);
  }

  addTransaction(transaction: any) {
    return this.http.post(this.baseUrl + 'transactions', transaction);
  }
  updateTransaction(transaction: any) {
    return this.http.patch(this.baseUrl + 'transactions', transaction);
  }
  deleteTransaction(id: number) {
    return this.http.delete(this.baseUrl + 'transactions/' + id);
  }

  getTransactions(txnParams: TransactionParams) {
    var params = getPaginationHeaders(txnParams.pageNumber, txnParams.pageSize);
    
    if (txnParams.search) {
      params = params.append('search', txnParams.search);
    }
    if (txnParams.month) {
      params = params.append('month', txnParams.month.toString());
    }
    if (txnParams.year) {
      params = params.append('year', txnParams.year.toString());
    }
    if (txnParams.paidBy) {
      params = params.append('paidBy', txnParams.paidBy.toString());
    }
    if (txnParams.paidTo) {
      params = params.append('paidTo', txnParams.paidTo.toString());
    }
    return getPaginatedResult<Transaction>(
      this.http,
      this.baseUrl + 'transactions',
      params
    );
  }
}
