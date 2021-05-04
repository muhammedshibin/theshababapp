import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Inmate } from '../shared/models/inmate';
import { getPaginatedResult, getPaginationHeaders } from '../shared/functions/pagination_functions';

@Injectable({
  providedIn: 'root'
})
export class TenantService {

  baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) { }

  getInmates(pageNumber: number , pageSize: number){
    let params = getPaginationHeaders(pageNumber,pageSize);
    return getPaginatedResult<Inmate>(this.http,this.baseUrl+'Inmates',params);
  }

  AddInmate(value: any){
    return this.http.post(this.baseUrl +'Inmates',value);
  }
}
