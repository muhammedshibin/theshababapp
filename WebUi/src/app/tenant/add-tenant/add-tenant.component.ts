import { TenantService } from './../tenant.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-tenant',
  templateUrl: './add-tenant.component.html',
  styleUrls: ['./add-tenant.component.scss']
})
export class AddTenantComponent implements OnInit {

  tenantRegistrationForm: FormGroup;

  constructor(private tenantService: TenantService) { }

  ngOnInit(): void {
    this.createRegistrationForm();
  }

  createRegistrationForm(){
    this.tenantRegistrationForm = new FormGroup({
      fullName: new FormControl('',Validators.required),
      email: new FormControl('',Validators.required),         
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
    },(err) => {
      console.log(err);
    })
  }

}
