import { BreadcrumbService } from 'xng-breadcrumb';
import { Vendor } from 'src/app/shared/models/vendor';
import { VendorService } from './../vendor.service';
import { ActivatedRoute } from '@angular/router';
import { TransactionParams } from './../../shared/models/transactionParams';
import { TransactionService } from './../../transactions/transaction.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vendor-detail',
  templateUrl: './vendor-detail.component.html',
  styleUrls: ['./vendor-detail.component.scss'],
})
export class VendorDetailComponent implements OnInit {
  txnParams = new TransactionParams();
  vendor: Vendor;

  constructor(
    private transactionService: TransactionService,
    private activatedRoute: ActivatedRoute,
    private vendorService: VendorService,
    private bcService: BreadcrumbService
  ) {}

  ngOnInit(): void {
    this.loadVendor();
  }

  loadVendor() {
    const id = +this.activatedRoute.snapshot.paramMap.get('id');
    this.vendorService.getVendor(id).subscribe(
      (data) => {
        this.vendor = data;
        this.bcService.set('@vendorName',this.vendor.name);
      },
      (err) => {
        console.log(err);
      }
    );
  }
}
