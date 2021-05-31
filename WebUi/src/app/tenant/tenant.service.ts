import { Inmate } from './../shared/models/inmate';
import { Leave } from './../shared/models/leave';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { getPaginatedResult, getPaginationHeaders } from '../shared/functions/pagination_functions';
import { InmateBill } from '../shared/models/inmateBill';

@Injectable({
  providedIn: 'root'
})
export class TenantService {

  baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) { }

  getInmates(pageNumber: number , pageSize: number , search: string){
    let params = getPaginationHeaders(pageNumber,pageSize);

    if(search){
      params = params.append('search',search);
    }

    return getPaginatedResult<Inmate>(this.http,this.baseUrl+'Inmates',params);
  }

  getInmate(id: number){
    return this.http.get<Inmate>(this.baseUrl + 'Inmates/' + id);
  }

  AddInmate(value: any){
    return this.http.post<Inmate>(this.baseUrl +'Inmates',value);
  }

  AddInmatePhoto(value: any,inmateId: number){
    const formData = new FormData();
    formData.append('file',value);
    return this.http.patch(this.baseUrl +'Inmates/photo/'+inmateId,formData);
  }

  getTenantBills(inmateId: number){
    return this.http.get<InmateBill[]>(this.baseUrl + 'bills/inmate/' + inmateId);
  }  

  updateInmate(inmate: Inmate){
    return this.http.patch(this.baseUrl +'Inmates',inmate);
  }

  getTenantLeaves(inmateId: number){
    return this.http.get<Leave[]>(this.baseUrl + 'leaves/tenant/' + inmateId);
  }

  addLeave(leave: Leave){
    return this.http.post(this.baseUrl + 'leaves' ,leave);
  }


}
