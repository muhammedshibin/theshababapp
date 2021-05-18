import { ToastrService } from 'ngx-toastr';
import { TenantService } from './../tenant.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-apply-leave',
  templateUrl: './apply-leave.component.html',
  styleUrls: ['./apply-leave.component.scss']
})
export class ApplyLeaveComponent implements OnInit {

  @Input() tenantId: number;
  leaveForm: FormGroup;
  @Output() leaveApplied = new EventEmitter<boolean>();

  constructor(private fb: FormBuilder , private tenantService: TenantService , private toastr: ToastrService) { }

  ngOnInit(): void {
    this.createLeaveForm();
  }

  createLeaveForm(){
    this.leaveForm = this.fb.group({
      inmateId:[this.tenantId,Validators.required],
      leaveReason:['',Validators.required],
      fromDate:[new Date(),Validators.required],
      toDate:[new Date(),Validators.required]
    })
  }

  onSubmit(){
    this.tenantService.addLeave(this.leaveForm.value).subscribe(response => {
      this.toastr.success('Leave Applied Successfully','Success');  
      this.leaveForm.reset();
      this.leaveApplied.emit(true);
    },(err) =>{
      this.toastr.error('Error Occured','Error');
    })
  }

}
