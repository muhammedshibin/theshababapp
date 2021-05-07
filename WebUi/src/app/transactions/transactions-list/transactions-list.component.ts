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

@Component({
  selector: 'app-transactions-list',
  templateUrl: './transactions-list.component.html',
  styleUrls: ['./transactions-list.component.scss'],
})
export class TransactionsListComponent implements OnInit {
  @ViewChild('searchBox') search: ElementRef;

  pageNumber = 1;
  pageSize = 10;
  pagination: Pagination;

  transactions: Transaction[];

  modalRef: BsModalRef;
  alertmodalRef: BsModalRef;

  name: string;

  constructor(
    private transactionService: TransactionService,
    private modalService: BsModalService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadTransactions();
  }

  onSearch() {
    const searchTerm: string = this.search.nativeElement.value;
    this.loadTransactions(searchTerm);
  }

  onSuccessfulAdding(event: any) {
    this.modalRef.hide();
    this.loadTransactions();
  }

  loadTransactions(searchTerm?: string) {
    this.transactionService
      .getTransactions(this.pageNumber, this.pageSize, searchTerm)
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
      this.transactionService.deleteTransaction(txn.id).subscribe((success) => {
          this.toastr.success('Transaction Deleted Successfully','Success');
          this.loadTransactions();
      })
    })
  }
   

  onPageChange(event: any) {
    this.pageNumber = event.page;
    this.loadTransactions();
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }
}
