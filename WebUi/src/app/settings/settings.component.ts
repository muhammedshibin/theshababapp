import { ToastrService } from 'ngx-toastr';
import { Category } from './../shared/models/category';
import { TransactionService } from './../transactions/transaction.service';
import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AlertWindowComponent } from '../shared/components/alert-window/alert-window.component';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss'],
})
export class SettingsComponent implements OnInit {
  categories: Category[];
  editFlag: string;
  options = [
    { display: 'False', value: false },
    { display: 'True', value: true }
  ];
  alertmodalRef: BsModalRef;

  constructor(
    private transactionService: TransactionService,
    private toastr: ToastrService,
    private modalService: BsModalService
  ) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories() {
    this.transactionService.getTransactionCategories().subscribe(
      (data) => {
        this.categories = data;
      },
      (err) => {
        console.log(err);
      }
    );
  }

  saveChanges() {
    if (this.editFlag != undefined) {
      this.toastr.error('There are Unsaved Changes', 'Error');
      return;
    }

    this.transactionService
      .updateTransactionCategories(this.categories)
      .subscribe((response) => {
        this.toastr.success('Categories Updated Successfully', 'Success');
      });
  }

  editCategory(id: number) {
    this.editFlag = 'enableEdit-' + id;
  }

  editingCompleted(id: number) {
    let record = this.categories.find((c) => c.id === id);
    if (record) {
      if (record.name === '' || record.name == undefined) {
        this.toastr.error('Category must have a name'), 'Error';
        //this.categories.splice(id-1,1);
      } else {
        record.name.toUpperCase();       
        this.editFlag = undefined;
        console.log(this.categories);
      }
    }
  }

  addCategory() {
    const categoryNextId = this.categories.length + 1;
    this.categories = [
      ...this.categories,
      {
        id: categoryNextId,
        name: '',
        isApplicableForVisitors: true,
        needToConsiderDays: false,
        defaultRate: 0,
        considerDefaultRate: false,
        coreCategory: false,
      },
    ];
    this.editCategory(this.categories.length);
  }

  deleteCategory(id: number){
    const initialState = {
      text:'Are you sure you want to delete this Category'
    };
    this.alertmodalRef = this.modalService.show(AlertWindowComponent, {initialState});
    this.alertmodalRef.onHidden.subscribe(() => {
      if(this.alertmodalRef.content.message !== 'Declined!'){
        this.transactionService.deleteTransactionCategory(id).subscribe((response) => {
          this.toastr.success('Category Deleted Successfully','Success');
          this.loadCategories();
       },err => {
         this.toastr.error('Error Occured, Please try again Later','Error Occured');
       })
      }
      else{
        return;
      }
      
    
  });
}
}
