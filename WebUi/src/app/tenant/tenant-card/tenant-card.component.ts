import { Inmate } from './../../shared/models/inmate';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-tenant-card',
  templateUrl: './tenant-card.component.html',
  styleUrls: ['./tenant-card.component.scss']
})
export class TenantCardComponent implements OnInit {

  @Input() tenant: Inmate;

  constructor() { }

  ngOnInit(): void {
  }

}
