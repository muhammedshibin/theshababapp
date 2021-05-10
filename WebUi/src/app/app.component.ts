import { AccountService } from './core/services/acount.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  ngOnInit(): void {
   this.loadCurrentUser();
  }
  title = 'Shabab App';

  constructor(private accountService: AccountService){}

  loadCurrentUser(){
    let token = localStorage.getItem('token');
    if(token){
      this.accountService.getCurrentUser(token).subscribe((response) =>{
        console.log('login successful');
      },err => {
        console.log('error occured');
      })
    }
  }
}
