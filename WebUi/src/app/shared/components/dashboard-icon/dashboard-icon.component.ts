import { Router } from '@angular/router';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard-icon',
  templateUrl: './dashboard-icon.component.html',
  styleUrls: ['./dashboard-icon.component.scss']
})
export class DashboardIconComponent implements OnInit {

  @Input() logoClass: string;
  @Input() logoName: string;
  @Input() logoLink: string;
  @Input() logoColour: string;

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  moveTo(url: string){
    this.router.navigateByUrl(url);
  }

}
