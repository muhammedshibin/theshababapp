import { map } from 'rxjs/operators';
import { AccountService } from './../services/acount.service';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private accountService: AccountService , private toastr:ToastrService , private router: Router){}

  canActivate(): Observable<boolean> {
    return this.accountService.currentUser$.pipe(
      map((user) => {
       
        if(user) return true;  
          
        this.toastr.error('Please login');
        
        this.router.navigateByUrl('');

        return false;
      })      
    );
  }
  
}
