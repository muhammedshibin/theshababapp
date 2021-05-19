import { Feedback } from './../shared/models/feedback';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {

  baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) { }

  postFeedback(feedback: Feedback){
    return this.http.post(this.baseUrl + 'feedback',feedback);
  }

  getAllFeedbacks(){
    return this.http.get<Feedback[]>(this.baseUrl + 'feedback');
  }
}
