import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { AccountService} from './../services/acount.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/shared/models/user';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

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
