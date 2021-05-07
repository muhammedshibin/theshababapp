import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-alert-window',
  templateUrl: './alert-window.component.html',
  styleUrls: ['./alert-window.component.scss']
})
export class AlertWindowComponent {

  confirmAction: boolean = false;
  message: string;
  text: string;

  constructor(public modalRef: BsModalRef) {} 
  
 
  confirm(): void {
    this.message = 'Confirmed!';
    this.confirmAction = true;
    this.modalRef.hide();
  }
 
  decline(): void {
    this.message = 'Declined!';
    this.modalRef.hide();
  }

}
