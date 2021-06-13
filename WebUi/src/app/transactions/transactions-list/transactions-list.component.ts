import { TransactionParams } from './../../shared/models/transactionParams';
import { AlertWindowComponent } from './../../shared/components/alert-window/alert-window.component';
import { Validators } from '@angular/forms';
import { TransactionService } from './../transaction.service';
import { Pagination } from './../../shared/models/pagination';
import { Transaction } from './../../shared/models/transaction';
import {
  Component,
  ElementRef,
  OnInit,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { getMonthArray, getYears } from 'src/app/shared/functions/month_function';


@Component({
  selector: 'app-transactions-list',
  templateUrl: './transactions-list.component.html',
  styleUrls: ['./transactions-list.component.scss'],
})
export class TransactionsListComponent implements OnInit {
  @ViewChild('searchBox') search: ElementRef;

  transactionParams = new TransactionParams();
  
  pagination: Pagination;

  transactions: Transaction[];

  modalRef: BsModalRef;
  alertmodalRef: BsModalRef;
  monthNames = getMonthArray();
  years = getYears();
  order = true;
  sortOptions = [
    {value:'amount',display:'Amount'},
    {value:'txndate',display:'Transaction Date'},
    {value:'category',display:'Category'},
    {value:'name',display:'Transaction Name'},
  ]

  selectedSort = 'txndate';

  name: string;

  constructor(
    private transactionService: TransactionService,
    private modalService: BsModalService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.transactionParams.month = new Date().getMonth() + 1;
    this.transactionParams.year = new Date().getFullYear();
    this.loadTransactions();
  }

  onSearch() {
   this.transactionParams.search= this.search.nativeElement.value;
    this.loadTransactions();
  }

  onSuccessfulAdding(event: any) {
    this.modalRef.hide();
    this.loadTransactions();
  }

  loadTransactions() {
    this.transactionService
      .getTransactions(this.transactionParams)
      .subscribe(
        (data) => {
          this.transactions = data.result;
          this.pagination = data.pagination;
        },
        (err) => {
          console.log(err);
        }
      );
  }

  editTransaction(id: number) {
    this.router.navigateByUrl('/transactions/'+id);
  }
  deleteTransaction(txn: Transaction ) {
    const initialState = {
      text:'Are you sure you want to delete this transaction'
    };
    this.alertmodalRef = this.modalService.show(AlertWindowComponent, {initialState});
    this.alertmodalRef.onHidden.subscribe(() => {
      if(this.alertmodalRef.content.message !== 'Declined!')
      this.transactionService.deleteTransaction(txn.id).subscribe((success) => {
          this.toastr.success('Transaction Deleted Successfully','Success');
          this.loadTransactions();
      })
    })
  }
   

  onPageChange(event: any) {
    this.transactionParams.pageNumber = event.page;
    this.loadTransactions();
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }  

  onMonthChange(event: any){
    this.loadTransactions();
  }

  onYearChange(event: any){
    this.loadTransactions();
  }

  onSortChange(event: any){
    const sortByOrder = this.order? 'desc':'asc';
    const sortBy = `${this.selectedSort}${sortByOrder}`;
    this.transactionParams.sortBy = sortBy;
    this.loadTransactions();
  }
}


