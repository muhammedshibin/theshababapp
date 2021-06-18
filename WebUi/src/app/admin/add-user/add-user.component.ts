import { AccountService } from './../../core/services/acount.service';
import { RegisterUser } from './../../shared/models/register-user';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.scss']
})
export class AddUserComponent implements OnInit {

  errors: any[] = [];

  registerUser: RegisterUser = new RegisterUser();

  constructor(private accountService: AccountService ,private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit(){
    this.accountService.registerUser(this.registerUser).subscribe((success) => {
      this.toastr.success('New User Added Successfully','Success');
      this.registerUser = new RegisterUser();
    },err => {
      console.log(err);
      if(Array.isArray(err))
        this.errors = err;
    })
   
  }

}
