import { TenantService } from './../tenant.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-tenant',
  templateUrl: './add-tenant.component.html',
  styleUrls: ['./add-tenant.component.scss']
})
export class AddTenantComponent implements OnInit {

  tenantRegistrationForm: FormGroup;

  constructor(private tenantService: TenantService , private toast: ToastrService , private router: Router) { }

  ngOnInit(): void {
    this.createRegistrationForm();
  }

  createRegistrationForm(){
    this.tenantRegistrationForm = new FormGroup({
      fullName: new FormControl('',Validators.required),
      emailAddress: new FormControl('',Validators.required),         
      phoneNumber: new FormControl('',Validators.required),
      dateOfBirth: new FormControl('',Validators.required) ,
      address: new FormControl('',Validators.required),   
      isVisit: new FormControl('',Validators.required),   
      isInmateOnTopBed: new FormControl(true,Validators.required),   
      inmatePhoto: new FormControl()
    });
  }

  addTenant(){
    this.tenantService.AddInmate(this.tenantRegistrationForm.value).subscribe((response) => {
      console.log(response);
      this.toast.success('Inmate Added Successfully' , 'Success');
      this.router.navigateByUrl('/tenants/list');
    },(err) => {
      console.log(err);
    })
  }

}
