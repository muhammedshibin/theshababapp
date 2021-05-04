import { Pagination } from './../../shared/models/pagination';
import { TenantService } from './../tenant.service';
import { Inmate } from './../../shared/models/inmate';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-tenant-list',
  templateUrl: './tenant-list.component.html',
  styleUrls: ['./tenant-list.component.scss']
})
export class TenantListComponent implements OnInit {

  tenants: Inmate[] = [];
  pageNumber = 1;
  pageSize = 5;
  pagination: Pagination

  constructor(private tenantService: TenantService) { }

  ngOnInit(): void {
    this.loadInmates();
  }

  loadInmates(){
    this.tenantService.getInmates(this.pageNumber,this.pageSize).subscribe((response) => {
      this.tenants = response.result;
      this.pagination = response.pagination;
      this.pageNumber = this.pagination.pageIndex;
      this.pageSize = this.pagination.pageSize;
    }, (err) => {
      console.log(err);
    })
  }

  onPageChange(event: any){
    this.pageNumber = event.page;
    if(this.pageNumber !== event)
      this.loadInmates();
  }

}
