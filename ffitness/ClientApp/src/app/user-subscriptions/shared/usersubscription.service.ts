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


  getCurrentUserSubscriptions(): Observable<UserSubscription[]> {
    return this.httpClient.get<UserSubscription[]>(this.getApiUrl() + '/user');
  }

  createUserSubscription(subscription: UserSubscription): Observable<UserSubscription> {
    return this.httpClient.post<UserSubscription>(this.getApiUrl() + '/user/' + subscription.subscriptionId, subscription);
  }

  delete(subscription: UserSubscription): Observable<UserSubscription> {
    const url = `${this.getApiUrl()}/${subscription.id}`;
    return this.httpClient
      .delete<UserSubscription>(url);
  }

  save(subscription: UserSubscription): Observable<UserSubscription> {
    return this.httpClient.post<UserSubscription>(this.getApiUrl(), subscription);
  }

  calcDaysBetween(from: number, until: number) {
    const oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds

    return Math.round((until - from) / oneDay);
  }
}
