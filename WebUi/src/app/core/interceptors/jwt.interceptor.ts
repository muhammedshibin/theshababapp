import { AccountService } from './../services/acount.service';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable} from 'rxjs';
import { take } from 'rxjs/operators';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  currentUser: any;

  constructor(private accountService: AccountService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
   

    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.currentUser = user);

    if(this.currentUser){
      request = request.clone({
        setHeaders:{
          Authorization:`Bearer ${this.currentUser.token}`
        }
      });
    }

    return next.handle(request);
  }
}
