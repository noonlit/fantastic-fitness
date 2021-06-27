import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Inject } from '@angular/core';
import { UserSubscription } from './usersubscription.model';

@Injectable({
  providedIn: 'root'
})

export class UserSubscriptionComponentService {
  private apiUrl;

  constructor(private httpClient: HttpClient, @Inject('API_URL') apiUrl: string) {
    this.apiUrl = apiUrl;
  }

  private getApiUrl() {
    return this.apiUrl + 'usersubscriptions';
  }

  getUserSubscriptions(): Observable<UserSubscription[]> {
    return this.httpClient.get<UserSubscription[]>(this.getApiUrl());
  }

  createUserSubscription(subscription: UserSubscription): Observable<UserSubscription> {
    return this.httpClient.post<UserSubscription>(this.getApiUrl() + '/User/' + subscription.subscriptionId, subscription);
  }
}
