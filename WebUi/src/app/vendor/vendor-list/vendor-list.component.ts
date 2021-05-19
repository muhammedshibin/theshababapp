import { VendorService } from './../vendor.service';
import { Vendor } from './../../shared/models/vendor';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vendor-list',
  templateUrl: './vendor-list.component.html',
  styleUrls: ['./vendor-list.component.scss']
})
export class VendorListComponent implements OnInit {

  vendors: Vendor[];

  constructor(private vendorService: VendorService) { }

  ngOnInit(): void {
    this.loadVendors();
  }

  loadVendors(){
    this.vendorService.getVendors().subscribe(data => {
      this.vendors = data;
    },err => {
      console.log(err);
    })
  }

}
