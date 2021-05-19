import { FeedbackService } from './feedback.service';
import { Feedback } from './../shared/models/feedback';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.scss']
})
export class FeedbackComponent implements OnInit {

  feedbacks: Feedback[] = [];
  @ViewChild('feedbackComment') feedbackMessage: ElementRef;

  constructor(private feedbackService: FeedbackService) { }

  ngOnInit(): void {
    this.loadFeedbacks();
  }

  loadFeedbacks(){
    this.feedbackService.getAllFeedbacks().subscribe((data) => {
      this.feedbacks = data;      
    }, err => {
      console.log(err);
    })
  }

  onSubmit(){
    this.feedbackService.postFeedback({message:this.feedbackMessage.nativeElement.value}).subscribe((response) =>{
      this.loadFeedbacks();
      this.feedbackMessage.nativeElement.value = '';
    },err => {
      console.log(err);
    })
  }

}
