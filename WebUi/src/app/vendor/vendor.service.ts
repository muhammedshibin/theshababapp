import { Vendor } from './../shared/models/vendor';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

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

  addVendor(vendor: any){
    const input = {
      name: vendor
    }
    return this.http.post(this.baseUrl + 'vendors',input);
  }

  updateVendor(vendor: Vendor){
    return this.http.patch(this.baseUrl + 'vendors',vendor);
  }
}
