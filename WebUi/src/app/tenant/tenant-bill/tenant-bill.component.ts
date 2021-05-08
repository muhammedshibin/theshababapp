import { InmateBill } from './../../shared/models/inmateBill';
import { Component, EventEmitter, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-tenant-bill',
  templateUrl: './tenant-bill.component.html',
  styleUrls: ['./tenant-bill.component.scss']
})
export class TenantBillComponent implements OnInit {

  @Input() inmateBill: InmateBill;
  customClass='';
  modalRef: BsModalRef;
  @Output() billPaid = new EventEmitter<boolean>();

  constructor(private modalService: BsModalService) { }

  ngOnInit(): void {
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  paymentSuccessEvent(event: any){
    this.modalRef.hide();
    this.billPaid.emit(true);
  }

}
