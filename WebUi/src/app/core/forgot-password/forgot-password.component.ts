import { ToastrModule, ToastrService } from 'ngx-toastr';
import { AccountService } from './../services/acount.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {

  email: string;

  constructor(private accountService: AccountService,private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  forgotPassword(){
    this.accountService.forgotPassword(this.email).subscribe(() => {
      this.toastr.success("Password Reset Link Sent","Success");
    })
  }

}
