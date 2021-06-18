import { RegisterUser } from './../../shared/models/register-user';
import { User } from './../../shared/models/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();
  baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) {}

  login(value: any) {
    return this.http.post(this.baseUrl + 'account/login', value).pipe(
      map((user: User) => {
        this.currentUserSource.next(user);
        localStorage.setItem('token', user.token);
      })
    );
  }

  
  getCurrentUser(token: string) {
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http
      .get(this.baseUrl + 'account/currentUser', { headers: headers })
      .pipe(
        map((user: User) => {
          user.roles = [];
          const roles = this.getDecodedToken(user.token).role;
          Array.isArray(roles)?user.roles = roles : user.roles.push(roles);
          this.currentUserSource.next(user);
          localStorage.setItem('token', user.token);
        })
      );
  }

  logout(){
    localStorage.removeItem('token');
    this.currentUserSource.next(null);    
  }

  initiate(){
    this.currentUserSource.next(null);  
  }

  getDecodedToken(token){
    return JSON.parse(atob(token.split('.')[1]));
  }

  registerUser(registerUser: RegisterUser){
    return this.http.post(this.baseUrl+'account/register',registerUser);
  }
}
