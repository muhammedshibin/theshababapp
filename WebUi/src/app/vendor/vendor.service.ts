import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Vendor } from '../shared/models/vendor';

@Injectable({
  providedIn: 'root'
})
export class VendorService {

  baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) { }

  getVendors(){
    return this.http.get<Vendor[]>(this.baseUrl + 'vendors');
  }
  getVendor(id: number){
    return this.http.get<Vendor>(this.baseUrl + 'vendors/'+ id);
  }
}
