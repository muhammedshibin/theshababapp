import { ToastrService } from 'ngx-toastr';
import { Pagination } from './../../shared/models/pagination';
import { BreadcrumbService } from 'xng-breadcrumb';
import { Vendor } from 'src/app/shared/models/vendor';
import { VendorService } from './../vendor.service';
import { ActivatedRoute } from '@angular/router';
import { TransactionParams } from './../../shared/models/transactionParams';
import { TransactionService } from './../../transactions/transaction.service';
import { Component, OnInit } from '@angular/core';
import { Transaction } from 'src/app/shared/models/transaction';

@Component({
  selector: 'app-vendor-detail',
  templateUrl: './vendor-detail.component.html',
  styleUrls: ['./vendor-detail.component.scss'],
})
export class VendorDetailComponent implements OnInit {
  txnParams = new TransactionParams();
  transactions: Transaction[];
  vendor: Vendor;
  pagination: Pagination;
  selectedMode = 'paidBy';
  editMode = false;

  constructor(
    private transactionService: TransactionService,
    private activatedRoute: ActivatedRoute,
    private vendorService: VendorService,
    private bcService: BreadcrumbService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadVendor();    
  }

  loadVendor() {
    const id = +this.activatedRoute.snapshot.paramMap.get('id');
    this.txnParams.paidBy = id;
    this.vendorService.getVendor(id).subscribe(
      (data) => {
        this.vendor = data;
        this.bcService.set('@vendorName',this.vendor.name);        
        this.loadTransations();
      },
      (err) => {
        console.log(err);
      }
    );
  }

  loadTransations(){
    this.transactionService.getTransactions(this.txnParams).subscribe(data => {
      this.transactions = data.result;
      this.pagination = data.pagination;
    })
  }
  onPageChange(event: any) {
    this.txnParams.pageNumber = event.page;
    this.loadTransations();
  }

  onModeSelection(mode: string){ 
    const id = +this.activatedRoute.snapshot.paramMap.get('id');  
    this.selectedMode = mode;
    if(this.selectedMode === 'paidBy'){
      this.txnParams.paidBy = id;
      this.txnParams.paidTo = null;
    }
    if(this.selectedMode === 'paidTo'){
      this.txnParams.paidTo = id;
      this.txnParams.paidBy = null;
    }
    this.loadTransations();
  }

  onSaveChanges(){
    this.vendorService.updateVendor(this.vendor).subscribe((response) => {
      this.toastr.success('Account Details Updated Successfully','Success');
    }, err => {
      console.log(err);
    })
  }

}
