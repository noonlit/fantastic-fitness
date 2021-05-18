import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from '@angular/core';
import { Observable } from "rxjs";
import { Activity } from "./activity.model";

@Injectable({
  providedIn: 'root'
})

export class ActivityComponentService {
  private apiUrl;

  constructor(private httpClient: HttpClient, @Inject('API_URL') apiUrl: string) {
    this.apiUrl = apiUrl;
  }

  private getApiUrl() {
    return this.apiUrl + 'activities';
  }

  public getActivities() : Observable<Activity[]> {
    return this.httpClient.get<Activity[]>(this.getApiUrl());
  }
}
