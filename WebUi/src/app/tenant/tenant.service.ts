import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Inmate } from '../shared/models/inmate';
import { getPaginatedResult, getPaginationHeaders } from '../shared/functions/pagination_functions';
import { InmateBill } from '../shared/models/inmateBill';

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

  getInmate(id: number){
    return this.http.get<Inmate>(this.baseUrl + 'Inmates/' + id);
  }

  AddInmate(value: any){
    return this.http.post(this.baseUrl +'Inmates',value);
  }

  getTenantBills(inmateId: number){
    return this.http.get<InmateBill[]>(this.baseUrl + 'bills/inmate/' + inmateId);
  }
}
