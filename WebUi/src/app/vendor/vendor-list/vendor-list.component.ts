import { ToastrService } from 'ngx-toastr';
import { VendorService } from './../vendor.service';
import { Vendor } from './../../shared/models/vendor';
import { Component, OnInit, ViewChild, ElementRef, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-vendor-list',
  templateUrl: './vendor-list.component.html',
  styleUrls: ['./vendor-list.component.scss']
})
export class VendorListComponent implements OnInit {

  vendors: Vendor[];
  vendorName: string;
  openForm = false;

  constructor(private vendorService: VendorService,private toastr: ToastrService,private modalService: BsModalService) { }

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

  createVendor(){
    this.vendorService.addVendor(this.vendorName).subscribe(() => {
      this.toastr.success('Account Added Successfully','Success');
      this.openForm = false;
      this.loadVendors();
    },err => {
      console.log(err);
    });
  }

  openModal() {
    this.openForm = true;
  }

}
