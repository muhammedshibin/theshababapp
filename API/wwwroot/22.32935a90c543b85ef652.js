(self.webpackChunkweb_ui=self.webpackChunkweb_ui||[]).push([[22],{5022:(t,e,i)=>{"use strict";i.r(e),i.d(e,{BillModule:()=>J});var n=i(6521),l=i(1116),o=i(7368),a=i(1636),s=i(3492),r=i(1462);function c(t,e){if(1&t&&(o.TgZ(0,"option",14),o._uU(1),o.qZA()),2&t){const t=e.$implicit,i=o.oxw();o.Q6J("value",i.monthNames.indexOf(t)+1),o.xp6(1),o.hij("",t," ")}}function g(t,e){if(1&t&&(o.TgZ(0,"li",28),o.TgZ(1,"div",29),o.TgZ(2,"p",30),o._uU(3),o.qZA(),o.TgZ(4,"p",30),o._uU(5),o.ALo(6,"currency"),o.qZA(),o.qZA(),o.qZA()),2&t){const t=e.$implicit;o.xp6(3),o.Oqu(t.transactionDetailName),o.xp6(2),o.Oqu(o.lcZ(6,2,t.amount))}}function d(t,e){if(1&t&&(o.TgZ(0,"tr"),o.TgZ(1,"td"),o._uU(2),o.qZA(),o.TgZ(3,"td"),o.TgZ(4,"ul",25),o.YNc(5,g,7,4,"li",26),o.qZA(),o.qZA(),o.TgZ(6,"td",27),o.TgZ(7,"h4"),o._uU(8),o.ALo(9,"currency"),o.qZA(),o.qZA(),o.qZA()),2&t){const t=e.$implicit;o.xp6(2),o.Oqu(t.categoryName),o.xp6(3),o.Q6J("ngForOf",t.transactionDetails),o.xp6(3),o.Oqu(o.lcZ(9,3,t.totalAmount))}}function u(t,e){if(1&t){const t=o.EpF();o.TgZ(0,"div"),o.TgZ(1,"div",15),o.TgZ(2,"table",16),o.TgZ(3,"tr",17),o.TgZ(4,"td"),o.TgZ(5,"strong"),o.TgZ(6,"h3"),o._uU(7,"Transactions For "),o.qZA(),o.qZA(),o.qZA(),o.TgZ(8,"td",18),o.TgZ(9,"h3"),o._uU(10),o.qZA(),o.qZA(),o._UZ(11,"td"),o.qZA(),o._UZ(12,"tr",19),o.YNc(13,d,10,5,"tr",20),o.TgZ(14,"tr"),o._UZ(15,"td"),o.TgZ(16,"td",21),o.TgZ(17,"h4"),o._uU(18,"Total"),o.qZA(),o.qZA(),o.TgZ(19,"td",22),o.TgZ(20,"h4"),o._uU(21),o.ALo(22,"currency"),o.qZA(),o.qZA(),o.qZA(),o.TgZ(23,"tr"),o._UZ(24,"td"),o.TgZ(25,"td",23),o.TgZ(26,"button",24),o.NdJ("click",function(){return o.CHM(t),o.oxw().onProceeding()}),o._uU(27,"Generate Individual Bills"),o.qZA(),o.qZA(),o.qZA(),o.qZA(),o.qZA(),o.qZA()}if(2&t){const t=o.oxw();o.xp6(10),o.AsE("",t.getMonthName(t.selectedMonth-1),",",t.selectedYear,""),o.xp6(3),o.Q6J("ngForOf",t.monthlyBill.categoryWiseExpenses),o.xp6(8),o.Oqu(o.lcZ(22,4,t.monthlyBill.subTotal))}}let Z=(()=>{class t{constructor(t,e){this.billService=t,this.toastr=e,this.monthNames=["January","February","March","April","May","June","July","August","September","October","November","December"],this.selectedMonth=(new Date).getUTCMonth(),this.selectedYear=(new Date).getUTCFullYear()}ngOnInit(){}onSubmit(){this.billService.generateMonthlyBills(this.selectedMonth,this.selectedYear).subscribe(t=>{this.monthlyBill=t},t=>{})}onProceeding(){this.billService.generateIndividualBills(this.selectedMonth,this.selectedYear).subscribe(t=>{this.toastr.success("Bills Generated Succesfully","Success")},t=>{this.toastr.error("Some Error occured","Error")})}getMonthName(t){return this.monthNames[t]}}return t.\u0275fac=function(e){return new(e||t)(o.Y36(a.C),o.Y36(s._W))},t.\u0275cmp=o.Xpm({type:t,selectors:[["app-generate-bill"]],decls:21,vars:4,consts:[[1,"text-center"],[1,"container"],[3,"ngSubmit"],["billForm","ngForm"],[1,"row"],[1,"col-4"],[1,"lead"],["name","selectedMonth",1,"form-control",3,"ngModel","ngModelChange"],[3,"value",4,"ngFor","ngForOf"],["for","year"],["type","number","name","selectedYear",1,"form-control",3,"ngModel","ngModelChange"],[1,"col-1"],[1,"btn","btn-primary","mt-1"],[4,"ngIf"],[3,"value"],[1,"container","mt-5"],[1,"table","table-hover"],[1,"bg-light"],[1,"text-center","text-danger","text-strong"],[1,"bg-info"],[4,"ngFor","ngForOf"],[1,"text-dark"],[1,"align-middle","text-success"],["colspan","2"],[1,"btn","btn-outline-danger","btn-lg",3,"click"],[1,"list-group"],["class","list-group-item list-group-item-secondary",4,"ngFor","ngForOf"],[1,"align-middle"],[1,"list-group-item","list-group-item-secondary"],[1,"d-flex","justify-content-between"],[1,"text-info"]],template:function(t,e){1&t&&(o.TgZ(0,"div"),o.TgZ(1,"h2",0),o._uU(2,"Generate Bill"),o.qZA(),o.TgZ(3,"div",1),o.TgZ(4,"form",2,3),o.NdJ("ngSubmit",function(){return e.onSubmit()}),o.TgZ(6,"div",4),o.TgZ(7,"div",5),o.TgZ(8,"h4",6),o._uU(9,"Select Month"),o.qZA(),o.TgZ(10,"select",7),o.NdJ("ngModelChange",function(t){return e.selectedMonth=t}),o.YNc(11,c,2,2,"option",8),o.qZA(),o.qZA(),o.TgZ(12,"div",5),o.TgZ(13,"label",9),o._uU(14,"Year"),o.qZA(),o.TgZ(15,"input",10),o.NdJ("ngModelChange",function(t){return e.selectedYear=t}),o.qZA(),o.qZA(),o.TgZ(16,"div",11),o._UZ(17,"label"),o.TgZ(18,"button",12),o._uU(19,"Generate"),o.qZA(),o.qZA(),o.qZA(),o.qZA(),o.YNc(20,u,28,6,"div",13),o.qZA(),o.qZA()),2&t&&(o.xp6(10),o.Q6J("ngModel",e.selectedMonth),o.xp6(1),o.Q6J("ngForOf",e.monthNames),o.xp6(4),o.Q6J("ngModel",e.selectedYear),o.xp6(5),o.Q6J("ngIf",e.monthlyBill))},directives:[r._Y,r.JL,r.F,r.EJ,r.JJ,r.On,l.sg,r.wV,r.Fj,l.O5,r.YN,r.Kr],pipes:[l.H9],styles:[""]}),t})();class p{constructor(t,e,i,n){this.month=i,this.year=n,this.pageNumber=t,this.pageSize=e}}var m=i(5470),h=i(1655),A=i(4532),b=i(7930);const f=["search"];function q(t,e){if(1&t){const t=o.EpF();o.TgZ(0,"button",12),o.NdJ("click",function(){return o.CHM(t),o.oxw().onFilterChange()})("ngModelChange",function(e){return o.CHM(t),o.oxw().billParams.paidPredicate=e}),o._uU(1),o.qZA()}if(2&t){const t=e.$implicit,i=o.oxw();o.s9C("btnRadio",t.value),o.Q6J("ngModel",i.billParams.paidPredicate),o.xp6(1),o.Oqu(t.display)}}function T(t,e){if(1&t&&(o.TgZ(0,"p",24),o._uU(1),o.qZA()),2&t){const t=o.oxw(2);o.xp6(1),o.AsE("Showing ",t.inmateBills.length," of ",t.pagination.count," items")}}function v(t,e){if(1&t){const t=o.EpF();o.TgZ(0,"div"),o.TgZ(1,"button",27),o.NdJ("click",function(){o.CHM(t);const e=o.oxw().$implicit,i=o.oxw(2),n=o.MAs(14);return i.payBill(e,n)}),o._uU(2,"Pay"),o.qZA(),o.qZA()}}function x(t,e){if(1&t&&(o.TgZ(0,"tr"),o.TgZ(1,"td",25),o.TgZ(2,"strong"),o._uU(3),o.qZA(),o.qZA(),o.TgZ(4,"td",26),o._uU(5),o.qZA(),o.TgZ(6,"td",26),o._uU(7),o.ALo(8,"currency"),o.qZA(),o.TgZ(9,"td",26),o._uU(10),o.qZA(),o.TgZ(11,"td",26),o.YNc(12,v,3,0,"div",8),o.qZA(),o.qZA()),2&t){const t=e.$implicit;o.xp6(3),o.hij(" ",t.inmate," "),o.xp6(2),o.AsE(" ",t.month," , ",t.year," "),o.xp6(2),o.hij(" ",o.lcZ(8,6,t.billAmount)," "),o.xp6(3),o.hij(" ",0===t.paymentStatus?"Not Paid":"Paid"," "),o.xp6(2),o.Q6J("ngIf",0===t.paymentStatus)}}function y(t,e){if(1&t){const t=o.EpF();o.TgZ(0,"div"),o.TgZ(1,"div",13),o.TgZ(2,"div"),o.YNc(3,T,2,2,"p",14),o.qZA(),o.TgZ(4,"div"),o.TgZ(5,"div",15),o.TgZ(6,"input",16,17),o.NdJ("keyup.enter",function(){return o.CHM(t),o.oxw().onSearch()}),o.qZA(),o.qZA(),o.qZA(),o.qZA(),o.TgZ(8,"div",18),o.TgZ(9,"table",19),o.TgZ(10,"thead"),o.TgZ(11,"tr"),o.TgZ(12,"th",20),o.TgZ(13,"div",21),o._uU(14," Name "),o.qZA(),o.qZA(),o.TgZ(15,"th",20),o.TgZ(16,"div",22),o._uU(17," Period "),o.qZA(),o.qZA(),o.TgZ(18,"th",20),o.TgZ(19,"div",22),o._uU(20," Amount "),o.qZA(),o.qZA(),o.TgZ(21,"th",20),o.TgZ(22,"div",22),o._uU(23," Status "),o.qZA(),o.qZA(),o.TgZ(24,"th",20),o.TgZ(25,"div",22),o._uU(26," Action "),o.qZA(),o.qZA(),o.qZA(),o.qZA(),o.TgZ(27,"tbody"),o.YNc(28,x,13,8,"tr",23),o.qZA(),o.qZA(),o.qZA(),o.qZA()}if(2&t){const t=o.oxw();o.xp6(3),o.Q6J("ngIf",t.inmateBills.length>0),o.xp6(25),o.Q6J("ngForOf",t.inmateBills)}}function M(t,e){if(1&t){const t=o.EpF();o.TgZ(0,"div",1),o.TgZ(1,"div",28),o.TgZ(2,"pagination",29),o.NdJ("pageChanged",function(e){return o.CHM(t),o.oxw().onPageChange(e)}),o.qZA(),o.qZA(),o.qZA()}if(2&t){const t=o.oxw();o.xp6(2),o.Q6J("boundaryLinks",!0)("itemsPerPage",t.pagination.pageSize)("totalItems",t.pagination.count)}}function B(t,e){if(1&t){const t=o.EpF();o.TgZ(0,"div",30),o.TgZ(1,"h4",31),o._uU(2,"Pay Bill"),o.qZA(),o.TgZ(3,"button",32),o.NdJ("click",function(){return o.CHM(t),o.oxw().modalRef.hide()}),o.TgZ(4,"span",33),o._uU(5,"\xd7"),o.qZA(),o.qZA(),o.qZA(),o.TgZ(6,"div",34),o.TgZ(7,"app-bill-payment",35),o.NdJ("paymentSuccess",function(e){o.CHM(t);const i=o.oxw(),n=o.MAs(14);return i.onPaymentSuccess(e,n)}),o.qZA(),o.qZA()}if(2&t){const t=o.oxw();o.xp6(7),o.Q6J("inputAmount",t.selectedBill.billAmount)("inputInmateId",t.selectedBill.inmateId)("inputBillId",t.selectedBill.id)}}let N=(()=>{class t{constructor(t,e){this.billService=t,this.modalService=e,this.inmateBills=[],this.billParams=new p(1,5,null,null),this.filtersList=[{value:void 0,display:"All"},{value:"NotPaid",display:"Not Paid"},{value:"PartiallyPaid",display:"Not Fully Paid"},{value:"Paid",display:"Paid"}]}ngOnInit(){this.billParams.paidPredicate="NotPaid",this.loadBills()}loadBills(){this.billService.getBills(this.billParams).subscribe(t=>{this.inmateBills=t.result,this.pagination=t.pagination})}onPageChange(t){this.billParams.pageNumber=t.page,this.loadBills()}openModal(t){this.modalRef=this.modalService.show(t)}payBill(t,e){this.selectedBill=t,this.modalRef=this.modalService.show(e)}onPaymentSuccess(t,e){this.loadBills(),this.modalRef.hide()}onFilterChange(){this.billParams.pageNumber=1,this.billParams.pageSize=5,this.loadBills()}onSearch(){this.billParams.search=this.search.nativeElement.value,this.billParams.pageNumber=1,this.billParams.pageSize=5,this.loadBills()}}return t.\u0275fac=function(e){return new(e||t)(o.Y36(a.C),o.Y36(m.tT))},t.\u0275cmp=o.Xpm({type:t,selectors:[["app-bill-list"]],viewQuery:function(t,e){if(1&t&&o.Gf(f,5),2&t){let t;o.iGM(t=o.CRH())&&(e.search=t.first)}},decls:15,vars:3,consts:[[1,"container-fluid"],[1,"row"],[1,"col-sm-1"],[1,"col-sm-10","py-2","mb-1"],[1,"d-flex","justify-content-between"],["name","paidPredicate",1,"btn-group"],["class","btn btn-outline-success btn-lg",3,"btnRadio","ngModel","click","ngModelChange",4,"ngFor","ngForOf"],["routerLink","/bills/add-bill",1,"btn","btn-info","btn-lg"],[4,"ngIf"],[1,"d-flex","justify-content-center"],["class","row",4,"ngIf"],["template",""],[1,"btn","btn-outline-success","btn-lg",3,"btnRadio","ngModel","click","ngModelChange"],[1,"d-flex","justify-content-between","mt-2"],["class","text-muted",4,"ngIf"],[1,"form-inline"],["type","text","placeholder","Search",1,"form-control",3,"keyup.enter"],["search",""],[1,"table-responsive","mt-1"],[1,"table"],[1,"border-0","bg-light"],[1,"p-2","text-left","text-uppercase"],[1,"p-2","text-center","text-uppercase"],[4,"ngFor","ngForOf"],[1,"text-muted"],[1,"align-middle","text-left"],[1,"align-middle","text-center"],[1,"btn","btn-success",3,"click"],[1,"col-xs-12","col-12"],[3,"boundaryLinks","itemsPerPage","totalItems","pageChanged"],[1,"modal-header"],[1,"modal-title","pull-left"],["type","button","aria-label","Close",1,"close","pull-right",3,"click"],["aria-hidden","true"],[1,"modal-body"],[3,"inputAmount","inputInmateId","inputBillId","paymentSuccess"]],template:function(t,e){1&t&&(o.TgZ(0,"div",0),o.TgZ(1,"div",1),o._UZ(2,"div",2),o.TgZ(3,"div",3),o.TgZ(4,"div",4),o.TgZ(5,"div"),o.TgZ(6,"div",5),o.YNc(7,q,2,3,"button",6),o.qZA(),o.qZA(),o.TgZ(8,"a",7),o._uU(9,"Generate Monthly Bill"),o.qZA(),o.qZA(),o.YNc(10,y,29,2,"div",8),o.qZA(),o.qZA(),o.TgZ(11,"div",9),o.YNc(12,M,3,3,"div",10),o.qZA(),o.qZA(),o.YNc(13,B,8,3,"ng-template",null,11,o.W1O)),2&t&&(o.xp6(7),o.Q6J("ngForOf",e.filtersList),o.xp6(3),o.Q6J("ngIf",e.inmateBills.length>0),o.xp6(2),o.Q6J("ngIf",e.pagination&&e.inmateBills.length>0))},directives:[l.sg,n.yS,l.O5,h.lz,r.JJ,r.On,A.Qt,b.p],pipes:[l.H9],styles:[""]}),t})();const _=[{path:"",component:N,data:{breadcrumb:"Bills"}},{path:"add-bill",component:Z,data:{breadcrumb:"Monthly Bill Generation"}},{path:"list",component:N,data:{breadcrumb:"Bills"}}];let w=(()=>{class t{}return t.\u0275fac=function(e){return new(e||t)},t.\u0275mod=o.oAB({type:t}),t.\u0275inj=o.cJS({imports:[[l.ez,n.Bz.forChild(_)],n.Bz]}),t})();var P=i(5425);let J=(()=>{class t{}return t.\u0275fac=function(e){return new(e||t)},t.\u0275mod=o.oAB({type:t}),t.\u0275inj=o.cJS({imports:[[l.ez,P.m,n.Bz],w]}),t})()}}]);