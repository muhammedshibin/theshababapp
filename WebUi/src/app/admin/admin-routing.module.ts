import { SharedModule } from './../shared/shared.module';
import { AdminGuard } from './../core/guards/admin.guard';
import { RouterModule, CanActivate } from '@angular/router';
import { AdminComponent } from './admin.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';


const route = [
  {path:'',component: AdminComponent,CanActivate:[AdminGuard]}
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,   
    RouterModule.forChild(route)
  ],
  exports:[RouterModule]
})
export class AdminRoutingModule { }
