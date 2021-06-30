import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Inject } from '@angular/core';
import { Subscription } from './subscription.model';

@Injectable({
  providedIn: 'root'
})

export class SubscriptionComponentService {
  private apiUrl;

  constructor(private httpClient: HttpClient, @Inject('API_URL') apiUrl: string) {
    this.apiUrl = apiUrl;
  }

  private getApiUrl() {
    return this.apiUrl + 'subscriptions';
  }

  getSubscriptions(): Observable<Subscription[]> {
    return this.httpClient.get<Subscription[]>(this.getApiUrl());
  }

  createSubscription(subscription: Subscription): Observable <Subscription> {
      return this.httpClient.post<Subscription>(this.getApiUrl(), subscription);
  }
}
