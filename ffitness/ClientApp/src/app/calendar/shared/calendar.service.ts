import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Inject } from '@angular/core';
import { ScheduledActivity } from './calendar.model';

@Injectable({
  providedIn: 'root'
})

export class CalendarComponentService {
  private apiUrl;

  constructor(private httpClient: HttpClient, @Inject('API_URL') apiUrl: string) {
    this.apiUrl = apiUrl;
  }

  private getApiUrl() {
    return this.apiUrl + 'scheduledactivities';
  }

  getScheduledActivities(): Observable<ScheduledActivity[]> {
    return this.httpClient
      .get<ScheduledActivity[]>(this.getApiUrl());
  }
}