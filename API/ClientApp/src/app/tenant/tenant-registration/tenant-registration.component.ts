import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-tenant-registration',
  templateUrl: './tenant-registration.component.html',
  styleUrls: ['./tenant-registration.component.css']
})
export class TenantRegistrationComponent implements OnInit {

  tenantForm: FormGroup;

  constructor() { }

  ngOnInit() {
    this.loadTenantForm();
  }

  loadTenantForm() {
    this.tenantForm = new FormGroup({
      fullName: new FormControl('', Validators.required),
      phoneNumber: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required,
        Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')]),
      address: new FormControl('',Validators.required)

      //bottomBed: new FormControl(true),
      //tenantType: new FormControl('')
    })
  }

  onSubmit(){
    console.log(this.tenantForm.value);
  }

}
