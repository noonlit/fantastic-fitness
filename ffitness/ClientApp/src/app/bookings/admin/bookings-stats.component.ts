import { ChangeDetectorRef, OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs/operators';
import { BookingStatsComponentService } from '../shared/booking-stats.service';

@Component({
  selector: 'app-bookings-stats',
  templateUrl: './bookings-stats.component.html',
  styleUrls: ['./bookings-stats.component.css']
})
export class BookingsStatsComponent implements OnInit {
  constructor(
    private bookingStatsService: BookingStatsComponentService,
    private cd: ChangeDetectorRef
  ) { }

  public bookedScheduledActivitiesStats = [];

  ngOnInit() {
    this.bookingStatsService.getBookedScheduledActivitiesStats().subscribe(result => {
      this.bookedScheduledActivitiesStats = result;
      console.log(result);
      this.cd.detectChanges();
    }, error => error => console.error(error));
  }
}
