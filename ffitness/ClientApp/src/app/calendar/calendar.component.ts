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
import { endOfHour } from 'date-fns/esm';
import { CalendarComponentService } from './shared/calendar.service';
import { ScheduledActivity } from './shared/calendar.model';
import { AuthorizeService } from '../../api-authorization/authorize.service';
import { map } from 'rxjs/operators';

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

  constructor(private cd: ChangeDetectorRef, private modal: NgbModal, private service: CalendarComponentService, private auth: AuthorizeService) {
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
              actions: this.actions,
              color: { primary: item.activity.colour, secondary: item.activity.colour },
              draggable: true,
              resizable: {
                beforeStart: true,
                afterEnd: true,
              },
            },
          ];
        }

        this.cd.detectChanges();

        // TODO remove this, but only show actions if user is logged in.
        this.auth.getUser().subscribe(data => {
          console.log(data); //You will get all your user related information in this field
        })

      }, error => console.error(error));
  }

  actions: CalendarEventAction[] = [
    {
      label: '<i class="fas fa-fw fa-pencil-alt"></i>',
      a11yLabel: 'Edit',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.handleEvent('Edited', event);
      },
    },
    {
      label: '<i class="fas fa-fw fa-trash-alt"></i>',
      a11yLabel: 'Delete',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.events = this.events.filter((iEvent) => iEvent !== event);
        this.handleEvent('Deleted', event);
      },
    },
  ];

  events: CalendarEvent<ScheduledActivity>[] = [];

  eventTimesChanged({
    event,
    newStart,
    newEnd,
  }: CalendarEventTimesChangedEvent): void {
    this.events = this.events.map((iEvent) => {
      if (iEvent === event) {
        return {
          ...event,
          start: newStart,
          end: newEnd,
        };
      }
      return iEvent;
    });
    this.handleEvent('Dropped or resized', event);
  }

  @ViewChild('modalContent', { static: true }) modalContent: TemplateRef<any>;

  modalData: {
    action: string;
    event: CalendarEvent;
  };

  handleEvent(action: string, event: CalendarEvent): void {
    this.modalData = { event, action };
    this.modal.open(this.modalContent, { size: 'lg' });
  }

  hourSegmentClicked(date: Date) {
    this.selectedDayViewDate = date;

    let event: CalendarEvent<ScheduledActivity> = {
      title: 'New event',
      start: date,
      end: endOfHour(date),
      color: { primary: "", secondary: "" },
      draggable: true,
      resizable: {
        beforeStart: true,
        afterEnd: true,
      },
    }

    let activity: ScheduledActivity = {
      startTime: event.start,
      endTime: endOfHour(date),
    }
    event.meta = activity;

    this.modalData = { action: "Schedule New Event", event: event };
    this.modal.open(this.modalContent, { size: 'lg' });
  }

  deleteEvent(eventToDelete: CalendarEvent) {
    this.events = this.events.filter((event) => event !== eventToDelete);
  }

  addEvent(eventToAdd: CalendarEvent): void {
    this.events = [
      ... this.events,
      eventToAdd
    ];
  }

  saveEvent(eventToSave: CalendarEvent) {
    console.log(eventToSave);
  }

  setView(view: CalendarView) {
    this.view = view;
  }

  closeOpenMonthViewDay() {
    this.activeDayIsOpen = false;
  }
}
