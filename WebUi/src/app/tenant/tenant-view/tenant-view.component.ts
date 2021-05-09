import { InmateBill } from './../../shared/models/inmateBill';
import { TenantService } from './../tenant.service';
import { Inmate } from './../../shared/models/inmate';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TabDirective } from 'ngx-bootstrap/tabs';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-tenant-view',
  templateUrl: './tenant-view.component.html',
  styleUrls: ['./tenant-view.component.scss'],
})
export class TenantViewComponent implements OnInit {
  inmate: Inmate;
  inmateBills: InmateBill[] = [];

  constructor(
    private activatedRoute: ActivatedRoute,
    private tenantService: TenantService,
    private bcService: BreadcrumbService
  ) {}

  ngOnInit(): void {    
    this.loadTenant();
  }

  loadTenant() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');

    this.tenantService.getInmate(parseInt(id)).subscribe(
      (data) => {
        this.inmate = data;
        this.bcService.set('@inmateName',this.inmate.fullName)
      },
      (err) => {
        console.log(err);
      }
    );
  }

  loadTenantBills() {
    this.tenantService.getTenantBills(this.inmate.id).subscribe(
      (data) => {
        this.inmateBills = data;
      },
      (err) => {
        console.log(err);
      }
    );
  }

  onSelect(tab: TabDirective) {
    if (tab.heading === 'Bill Details') {
      this.loadTenantBills();
      //this.loadTenant();
    }
  }  

  onBillPaid(event: any){
    this.loadTenantBills();
  }
}
