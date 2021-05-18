import { Leave } from './../../shared/models/leave';
import { Component, EventEmitter, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-tenant-leave',
  templateUrl: './tenant-leave.component.html',
  styleUrls: ['./tenant-leave.component.scss']
})
export class TenantLeaveComponent implements OnInit {

  @Input() leaves: Leave[];
  @Input() tenantId: number;
  @Output() leaveSuccessEvent = new EventEmitter<boolean>();
  modalRef: BsModalRef;


  constructor(private modalService: BsModalService) { }

  ngOnInit(): void {
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  leaveApplyStatus(event: any){
    console.log('leave applied');
    this.leaveSuccessEvent.emit(true);
    this.modalRef.hide();
  }

}
