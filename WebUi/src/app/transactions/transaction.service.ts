import { Transaction } from './../shared/models/transaction';
import { Category } from './../shared/models/category';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { getPaginatedResult, getPaginationHeaders } from '../shared/functions/pagination_functions';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) { }

  getTransactionCategories(){
    return this.http.get<Category[]>(this.baseUrl + 'transactions/categories');
  }

  getTransactionById(id: string){
    return this.http.get<Transaction>(this.baseUrl + 'transactions/'+id);
  }

  addTransaction(transaction: any){
    return this.http.post(this.baseUrl + 'transactions',transaction);
  }
  updateTransaction(transaction: any){
    return this.http.patch(this.baseUrl + 'transactions',transaction);
  }
  deleteTransaction(id: number){
    return this.http.delete(this.baseUrl + 'transactions/' + id );
  }

  getTransactions(pageNumber: number , pageSize: number , searchTerm?: string){
    var params = getPaginationHeaders(pageNumber,pageSize);
    if(searchTerm){
      params = params.append('search',searchTerm);
    }
    return getPaginatedResult<Transaction>(this.http,this.baseUrl + 'transactions' , params);
  }
}
