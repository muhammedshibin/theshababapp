import { Pagination } from './../../shared/models/pagination';
import { BillService } from './../bill.service';
import { BillParams } from './../../shared/models/billParams';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { InmateBill } from 'src/app/shared/models/inmateBill';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-bill-list',
  templateUrl: './bill-list.component.html',
  styleUrls: ['./bill-list.component.scss']
})
export class BillListComponent implements OnInit {

  inmateBills: InmateBill[] = [];
  pagination: Pagination;
  billParams = new BillParams(1,3,new Date().getMonth(),new Date().getFullYear());
  modalRef: BsModalRef;
  selectedBill: InmateBill;

  constructor(private billService: BillService , private modalService: BsModalService) { }

  ngOnInit(): void {
    this.billParams.month = 5;
    this.loadBills();
  }

  loadBills(){
    this.billService.getBills(this.billParams).subscribe((data) => {
      this.inmateBills = data.result;
      this.pagination = data.pagination;
    })
  }

  onPageChange(event: any){
    this.billParams.pageNumber = event.page;
    this.loadBills();
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  payBill(bill: InmateBill,template: TemplateRef<any>){
    this.selectedBill = bill;
    this.modalRef = this.modalService.show(template);
  }

}
