import { Transaction } from './../../shared/models/transaction';
import { TransactionService } from './../transaction.service';
import { Category } from './../../shared/models/category';
import { VendorService } from './../../vendor/vendor.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Vendor } from 'src/app/shared/models/vendor';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-transaction',
  templateUrl: './add-transaction.component.html',
  styleUrls: ['./add-transaction.component.scss'],
})
export class AddTransactionComponent implements OnInit {
  transaction: Transaction;
  transactionForm: FormGroup;
  vendors: Vendor[];
  paidToVendors: Vendor[];
  categories: Category[];
  currentDate = new Date();
  @Output() formSubmitted = new EventEmitter<boolean>();

  constructor(
    private vendorService: VendorService,
    private txnService: TransactionService,
    private activatedRouter: ActivatedRoute,
    private toastr: ToastrService,
    private router: Router,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.createTransationForm();
    this.loadVendors();
    this.loadCategories();
    this.getTransactionId();
  }

  getTransactionId() {
    const id = this.activatedRouter.snapshot.paramMap.get('id');
    if (id) {
      this.txnService.getTransactionById(id).subscribe(
        (data) => {
          this.transaction = data;
          this.fillTransactionDetails();
        },
        (err) => {
          console.log(err);
        }
      );
    }
  }

  createTransationForm() {
    this.transactionForm =  this.fb.group({
      id:[0],
      name: ['',Validators.required],
      transactionDate: [new Date(),Validators.required],
      isExpense: [true,Validators.required],
      paidPartyId: [1,Validators.required],
      paidToId: [2,Validators.required],
      categoryId: [1,Validators.required],
      amount: [null,[Validators.required]],
    });
  }

  onSubmit() {
    if (this.transaction) {
      this.txnService.updateTransaction(this.transactionForm.value).subscribe(
        (response) => {
          console.log(response);
          this.toastr.success('Transaction Saved Successfully' , 'Success');
          this.router.navigateByUrl('/transactions/list');
        },
        (err) => {
          console.log(err);
        }
      );
    } else {
      this.txnService.addTransaction(this.transactionForm.value).subscribe(
        (response) => {
          console.log(response);
          this.toastr.success('Transaction Saved Successfully' , 'Success');
          this.formSubmitted.emit(true);
        },
        (err) => {
          console.log(err);
        }
      );
    }
  }

  loadVendors() {
    this.vendorService.getVendors().subscribe(
      (response) => {
        this.vendors = response;
        // const billpaymentVendor = this.vendors.findIndex(c => c.id === 3);
        // if(billpaymentVendor !== -1){
        //   this.vendors.splice(billpaymentVendor,1);
        // }
        this.paidToVendors = [...this.vendors];
      },
      (err) => {
        console.log(err);
      }
    );
  }
  loadCategories() {
    this.txnService.getTransactionCategories().subscribe(
      (response) => {
        this.categories = response;
        // const depositIndex = this.categories.findIndex(c => c.name === 'DEPOSIT');
        // if(depositIndex !== -1){
        //   this.categories.splice(depositIndex,1);
        // }
        // const billpaymentIndex = this.categories.findIndex(c => c.name === 'BILLPAYMENT');
        // if(billpaymentIndex !== -1){
        //   this.categories.splice(billpaymentIndex,1);
        // }
      },
      (err) => {
        console.log(err);
      }
    );
  }

  fillTransactionDetails() {

    const patchDateValue = this.transaction?.transactionDate.toLocaleString();

    if (this.transaction) {

      const existingTransaction = {
        id:this.transaction.id,
        name:this.transaction.name,
        amount:this.transaction.amount,
        categoryId: this.transaction.categoryId,
        transactionDate: this.transaction.transactionDate,
        isExpense: this.transaction.isExpense,
        paidPartyId: this.transaction.paidPartyId,
        paidToId: this.transaction.paidToId
      }

      this.transactionForm.setValue(existingTransaction);
    }


  }
}
