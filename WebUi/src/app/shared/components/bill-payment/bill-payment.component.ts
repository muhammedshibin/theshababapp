import { BillService } from '../../../bill/bill.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-bill-payment',
  templateUrl: './bill-payment.component.html',
  styleUrls: ['./bill-payment.component.scss']
})
export class BillPaymentComponent implements OnInit {

  paymentForm: FormGroup;
  @Input() inputInmateId: number;
  @Input() inputBillId: number;
  @Input() inputAmount: number;
  @Output() paymentSuccess = new EventEmitter<boolean>();

  constructor(private fb: FormBuilder , private billService: BillService , private toastr: ToastrService) { }

  ngOnInit(): void {
    this.createPaymentForm();
  }

  createPaymentForm(){
    this.paymentForm = this.fb.group({      
      inmateId:['',Validators.required],
      paidOn:[new Date()],
      amount:[0,Validators.min(1)],
      billId:['']
    })

    this.paymentForm.patchValue({
      inmateId : this.inputInmateId,
      billId : this.inputBillId,
      amount: this.inputAmount
    })
  }

  onSubmit(){
    this.billService.payBill(this.paymentForm.value).subscribe((response) => {
      this.toastr.success('Payment Recorded Successfully' , 'Succecss');
      this.paymentForm.reset();
      this.paymentSuccess.emit(true);
    },err => {
      this.toastr.error('Error occured during Payment' , 'Error');
    })
  }

}
