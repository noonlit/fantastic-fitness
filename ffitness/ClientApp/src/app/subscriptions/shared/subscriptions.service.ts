import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from '@angular/core';
import { Observable } from "rxjs";
import { Subscription } from "./subscriptions.model";

@Injectable({
  providedIn: 'root'
})

/*export class SubscriptionsComponentService {
  private apiUrl;

  constructor(private httpClient: HttpClient, @Inject('API_URL') apiUrl: string) {
    this.apiUrl = apiUrl;
  }

  private getApiUrl() {
    return this.apiUrl + 'subscriptions';
  }

  public getSubscriptions(): Observable<Subscription[]> {
    return this.httpClient.get<Subscription[]>(this.getApiUrl());
  }
}*/
