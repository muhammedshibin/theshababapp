import { User } from 'src/app/shared/models/user';
import { take } from 'rxjs/operators';
import { AccountService } from './../../core/services/acount.service';
import { Directive, ViewContainerRef, TemplateRef, OnInit, Input } from '@angular/core';

@Directive({
  selector: '[appHasRole]',
})
export class HasRoleDirective implements OnInit{

  @Input() appHasRole: string[]

  user: User;

  constructor(
    private viewContainerRef: ViewContainerRef,
    private templateRef: TemplateRef<any>,
    private accountService: AccountService
  ) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.user = user;
    })
  }
  ngOnInit(): void {
    if(this.user == null &&!this.user.roles ){
      this.viewContainerRef.clear();
      return;
    }

    if(this.user.roles.some(r => this.appHasRole.includes(r))){
      this.viewContainerRef.createEmbeddedView(this.templateRef);
    }
   
  }
}
