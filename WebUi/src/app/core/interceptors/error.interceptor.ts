import { ToastrService } from 'ngx-toastr';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private toastr: ToastrService, private router: Router) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((err) => {
        if (err) {
          switch (err.status) {
            case 400:
              if (err.error?.errors?.length > 0) {
                const modelErrors = err.error.errors;
                throw modelErrors;
              } else {
                this.toastr.error(err.error.userMessage, 'Error');
              }
              break;
            case 401:
              this.toastr.error('You are not Authorized', 'Unauthorized');
              break;
            case 404:
              this.router.navigateByUrl('/not-found');
              break;
            default:
              console.log(err);
              this.toastr.error(err.error.userMessage, 'Error');
              break;
          }
        }
        return throwError(err);
      })
    );
  }
}
