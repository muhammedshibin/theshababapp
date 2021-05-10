import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { AccountService} from './../services/acount.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/shared/models/user';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  username: string;
  password: string;
  currentUser$: Observable<User>;

  constructor(private accountService: AccountService ,private router: Router) { }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
  }

  onLogin(){
    this.accountService.login({username:this.username,password:this.password}).subscribe(
      (response) =>{
        console.log('success');
        this.router.navigateByUrl('/transactions/list');
      },(err) =>{
        console.log(err);
      }
    )
  }

  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

}
