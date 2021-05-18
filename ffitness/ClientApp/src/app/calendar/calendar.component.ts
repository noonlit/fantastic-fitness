import { ChangeDetectorRef, Component, OnInit } from '@angular/core';

import {
  ChangeDetectionStrategy,
  ViewChild,
  TemplateRef,

} from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {
  CalendarEvent,
  CalendarView,
  DAYS_OF_WEEK
} from 'angular-calendar';
import { FlatpickrDefaultsInterface } from 'angularx-flatpickr/flatpickr-defaults.service';
import { CalendarComponentService } from './shared/calendar.service';
import { ScheduledActivity } from './shared/calendar.model';
import { BookingComponentService } from './shared/booking.service';
import { AuthorizeService } from '../../api-authorization/authorize.service';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Booking } from '../bookings/shared/booking.model';

@Component({
  selector: 'app-scheduled-activities',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CalendarComponent implements OnInit {

  activities: ScheduledActivity[];

  public startDatePickerOptions: FlatpickrDefaultsInterface = {
    allowInput: true,
    enableTime: true,
    mode: 'single',
    dateFormat: "Y-m-d H:i",
    minDate: 'today',
    altFormat: "F j, Y H:i",
    altInput: true,
    enable: [{ from: new Date(0, 1), to: new Date(new Date().getFullYear() + 200, 12) }],
  }

  public endDatePickerOptions: FlatpickrDefaultsInterface = {
    allowInput: true,
    enableTime: true,
    mode: 'single',
    dateFormat: "Y-m-d H:i",
    minDate: 'today',
    altFormat: "F j, Y H:i",
    altInput: true,
    enable: [{ from: new Date(0, 1), to: new Date(new Date().getFullYear() + 200, 12) }]
  }

  view: CalendarView = CalendarView.Week;
  viewDate: Date = new Date();
  daysInWeek = 7;
  selectedDayViewDate: Date;
  weekStartsOn = DAYS_OF_WEEK.MONDAY;
  activeDayIsOpen: boolean = true;
  isAuthenticated: Observable<boolean>;

  constructor(
    private cd: ChangeDetectorRef,
    private modal: NgbModal,
    private service: CalendarComponentService,
    private bookingService: BookingComponentService,
    private authorizeService: AuthorizeService) {
  }

  ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated();

    this.service.getScheduledActivities()
      .subscribe(result => {
        for (let item of result) {
          this.events = [
            ...this.events,
            {
              title: item.activity.name,
              start: new Date(item.startTime),
              end: new Date(item.endTime),
              color: { primary: item.activity.colour, secondary: item.activity.colour },
              meta: item
            },
          ];
        }

        this.cd.detectChanges();

      }, error => console.error(error));
  }

  events: CalendarEvent[] = [];

  @ViewChild('modalContent', { static: true }) modalContent: TemplateRef<any>;

  modalData: {
    event: CalendarEvent;
  };

  openModal(event: CalendarEvent): void {
    this.bookingService.getBookingForCurrentUserAndActivity(event.meta)
      .pipe(
        catchError(error => {
          return of([]);
        })
      )
      .subscribe(result => {
        event.meta.currentBooking = result;
        this.modalData = { event };
        this.modal.open(this.modalContent, { size: 'lg' });
        this.cd.detectChanges();

      }, error => error => console.error(error));
  }

  userHasBooking(event: CalendarEvent) {
    return event.meta.currentBooking != undefined &&
      event.meta.currentBooking != null &&
      Object.keys(event.meta.currentBooking).length > 0;
  }

  eventHasCapacity(event: CalendarEvent) {
    return event.meta.capacity > 0;
  }

  eventCanBeBooked(event: CalendarEvent) {
    return !this.userHasBooking(event) && this.eventHasCapacity(event) && event.start > new Date();
  }

  eventBookingCanBeCanceled(event: CalendarEvent) {
    return this.userHasBooking(event) && event.start > new Date();
  }

  bookEvent(event: CalendarEvent): void {
    this.bookingService.bookSpot(event.meta)
      .subscribe(result => {
        event.meta.currentBooking = result;
        event.meta.capacity--;
        this.modalData = { event };
        this.cd.detectChanges();
      }, error => console.error(error));
  }

  cancelEventBooking(event: CalendarEvent): void {
    this.bookingService.cancelBooking(event.meta.currentBooking)
      .subscribe(result => {
        event.meta.currentBooking = null;
        event.meta.capacity++;
        this.modalData = { event };
        this.cd.detectChanges();
      }, error => console.error(error));
  }

  setView(view: CalendarView) {
    this.view = view;
  }

  closeOpenMonthViewDay() {
    this.activeDayIsOpen = false;
  }
}
