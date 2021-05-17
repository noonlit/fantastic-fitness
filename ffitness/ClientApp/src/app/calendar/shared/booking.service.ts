import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Inject } from '@angular/core';
import { Booking } from '../../bookings/shared/booking.model';

@Injectable({
  providedIn: 'root'
})

export class BookingComponentService {
  private apiUrl;

  constructor(private httpClient: HttpClient, @Inject('API_URL') apiUrl: string) {
    this.apiUrl = apiUrl;
  }

  private getApiUrl() {
    return this.apiUrl + 'bookings';
  }

  bookSpot(scheduledActivity): Observable<Booking> {
    return this.httpClient.post<Booking>(this.getApiUrl() + '/BookSpot', scheduledActivity);
  }
}
