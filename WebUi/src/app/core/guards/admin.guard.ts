import { User } from 'src/app/shared/models/user';
import { map } from 'rxjs/operators';
import { AccountService } from './../services/acount.service';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {


constructor(private accountService: AccountService , private toastr: ToastrService,private router: Router) {}

  canActivate(): Observable<boolean> {
    return this.accountService.currentUser$.pipe(
      map((user: User) => {
        if(user.roles.includes('Admin')){
          return true;
        }
        this.toastr.error('You cannot enter this Area');    
        this.router.navigateByUrl('');
      })
    )
  }
  
}
