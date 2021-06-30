import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Inject } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class BookingStatsComponentService {
  private apiUrl;

  constructor(private httpClient: HttpClient, @Inject('API_URL') apiUrl: string) {
    this.apiUrl = apiUrl;
  }

  private getApiUrl() {
    return this.apiUrl + 'bookingstats';
  }

  getBookedScheduledActivitiesStats(): Observable<any> {
    return this.httpClient.get<any>(this.getApiUrl());
  }

  getPopularActivitiesStats(): Observable<any> {
    return this.httpClient.get<any>(this.getApiUrl() + "/PopularActivities");
  }

  getPopularTrainersStats(): Observable<any> {
    return this.httpClient.get<any>(this.getApiUrl() + "/PopularTrainers");
  }
}
