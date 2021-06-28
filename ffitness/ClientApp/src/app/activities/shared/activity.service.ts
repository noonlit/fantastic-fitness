import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from '@angular/core';
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
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

  public getActivity(id: number): Observable<Activity> {
    return this.httpClient.get<Activity>(this.getApiUrl() + '/' + id);
  }

  save(activity: Activity): Observable<Activity> {
    return this.httpClient.post<Activity>(this.getApiUrl(), activity);
  }

  update(activity): Observable<Activity> {
    const url = `${this.getApiUrl()}/${activity.id}`;
    return this.httpClient
      .put<Activity>(url, activity);
  }

  delete(activity: Activity): Observable<Activity> {
    const url = `${this.getApiUrl()}/${activity.id}`;
    return this.httpClient
      .delete<Activity>(url);
  }
}
