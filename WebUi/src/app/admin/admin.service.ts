import { User } from 'src/app/shared/models/user';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  private baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) { }

  getUsersWithRoles(){
    return this.http.get<Partial<User>[]>(this.baseUrl+'admin/user-with-roles');
  }

  updateUserRoles(userName:string,roles: string[]){
    return this.http.post(this.baseUrl + `admin/edit-user-roles/${userName}?roles=${roles}`,{});
  }
}
