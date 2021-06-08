import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Subscription } from './subscriptions.model';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionsService {

  private apiUrl: string;

  constructor(private httpClient: HttpClient, @Inject('API_URL') apiUrl: string) {
    this.apiUrl = apiUrl;
  }

  getSubscriptions(): Observable<Subscription[]> {
    return this.httpClient.get<Subscription[]>(this.apiUrl + 'subscription');
  }
}
