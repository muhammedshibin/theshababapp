import { Pagination } from './../../shared/models/pagination';
import { BillService } from './../bill.service';
import { BillParams } from './../../shared/models/billParams';
import { Component, ElementRef, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { InmateBill } from 'src/app/shared/models/inmateBill';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-bill-list',
  templateUrl: './bill-list.component.html',
  styleUrls: ['./bill-list.component.scss'],
})
export class BillListComponent implements OnInit {
  @ViewChild('search') search: ElementRef;
  inmateBills: InmateBill[] = [];
  pagination: Pagination;
  billParams = new BillParams(
    1,
    5,
    null,
    null
  );
  modalRef: BsModalRef;
  selectedBill: InmateBill;
  // sortByOptions = [
  //   {value : 'byNameAsc' , display: 'Inmate Ascending'},
  //   {value : 'byNameDesc' , display: 'Inmate Descending'}
  // ]
  filtersList = [
    { value: null, display: 'All' },
    { value: 'NotPaid', display: 'Not Paid' },
    { value: 'PartiallyPaid', display: 'Not Fully Paid' },
    { value: 'Paid', display: 'Paid' },
  ];

  constructor(
    private billService: BillService,
    private modalService: BsModalService
  ) {}

  ngOnInit(): void {
   
    this.billParams.paidPredicate = null;
    this.loadBills();
  }

  loadBills() {
    this.billService.getBills(this.billParams).subscribe((data) => {
      this.inmateBills = data.result;
      this.pagination = data.pagination;
    });
  }

  onPageChange(event: any) {
    this.billParams.pageNumber = event.page;
    this.loadBills();
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  payBill(bill: InmateBill, template: TemplateRef<any>) {
    this.selectedBill = bill;
    this.modalRef = this.modalService.show(template);
  }
  onPaymentSuccess(event: any, template: TemplateRef<any>) {
    this.loadBills();
    this.modalRef.hide();
  }
  onFilterChange(value: string){
    this.billParams.pageNumber =1;
    this.billParams.pageSize = 5;
    this.billParams.paidPredicate = value;
    this.loadBills();
  }
  onSearch(){
    this.billParams.search = this.search.nativeElement.value;
    this.billParams.pageNumber =1;
    this.billParams.pageSize = 5;
    this.loadBills();
  }
}
