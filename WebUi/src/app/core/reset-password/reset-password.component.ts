import { ToastrService } from 'ngx-toastr';
import { AccountService } from './../services/acount.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {

  passwordForm: FormGroup;
  token: string;
  errors: string[];

  constructor(private activatedroute: ActivatedRoute,private fb: FormBuilder,private accountService: AccountService , private toastr: ToastrService) { }

  ngOnInit(): void {
    this.activatedroute.queryParams.subscribe(params => {
      this.token = params['token'];
      console.log(this.token);      
    })
    this.initializeForm();
  }

  initializeForm(){
    this.passwordForm = this.fb.group({
      email:['',[Validators.required,Validators.email]],
      password:['',[Validators.required,Validators.minLength(6)]],
      confirmPassword:['',[Validators.required,Validators.minLength(6)]],
      token:['']
    })
  }

  onSubmit(){
    
    this.passwordForm.patchValue({
      token : this.token
    });

    this.accountService.resetPassword(this.passwordForm.value).subscribe(() => {
      this.toastr.success('Password Changed Successfully','Success');
      this.accountService.logout();
    },err => {
      if(Array.isArray(err))
        this.errors = err;
      console.log(err);
    })

  }



}
