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

  getCurrentUserSubscriptions(): Observable<UserSubscription[]> {
    return this.httpClient.get<UserSubscription[]>(this.getApiUrl() + '/user');
  }

  createUserSubscription(subscription: UserSubscription): Observable<UserSubscription> {
    return this.httpClient.post<UserSubscription>(this.getApiUrl() + '/user/' + subscription.subscriptionId, subscription);
  }
}
