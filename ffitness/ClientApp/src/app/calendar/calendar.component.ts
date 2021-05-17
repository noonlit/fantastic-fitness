import { ChangeDetectorRef, Component, Inject, Injectable, OnInit } from '@angular/core';

import {
  ChangeDetectionStrategy,
  ViewChild,
  TemplateRef,

} from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {
  CalendarEvent,
  CalendarEventAction,
  CalendarEventTimesChangedEvent,
  CalendarView,
  DAYS_OF_WEEK
} from 'angular-calendar';
import { FlatpickrDefaultsInterface } from 'angularx-flatpickr/flatpickr-defaults.service';
import { CalendarComponentService } from './shared/calendar.service';
import { ScheduledActivity } from './shared/calendar.model';
import { BookingComponentService } from './shared/booking.service';

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

  constructor(
    private cd: ChangeDetectorRef,
    private modal: NgbModal,
    private service: CalendarComponentService,
    private bookingService: BookingComponentService) {
  }

  ngOnInit() {
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
    this.modalData = { event };
    this.modal.open(this.modalContent, { size: 'lg' });
  }

  bookEvent(event: CalendarEvent): void {
    console.log(event);

    this.bookingService.bookSpot(event.meta)
      .subscribe(result => {
        console.log(result);
      }, error => console.error(error));
  }

  setView(view: CalendarView) {
    this.view = view;
  }

  closeOpenMonthViewDay() {
    this.activeDayIsOpen = false;
  }
}
