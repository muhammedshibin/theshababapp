import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { BreadcrumbService } from 'xng-breadcrumb';
import { AccountService } from '../core/services/acount.service';
import { User } from '../shared/models/user';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  username: string;
  password: string;
  currentUser$: Observable<User>;
  title$: Observable<any[]>;

  constructor(private accountService: AccountService ,private router: Router,private toastr:ToastrService,private bcService: BreadcrumbService) { }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
    this.title$ = this.bcService.breadcrumbs$;
  }

  onLogin(){
    this.accountService.login({username:this.username,password:this.password}).subscribe(
      (response) => {
        console.log('success');
        this.router.navigateByUrl('/dashboard');
      }
    )
  }

  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }




}
