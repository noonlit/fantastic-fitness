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
import { Activity } from '../../../activities/shared/activity.model';
import { ActivityComponentService } from '../../../activities/shared/activity.service';
import { ScheduledActivity } from '../../shared/calendar.model';
import { CalendarComponentService } from '../../shared/calendar.service';

@Component({
  selector: 'app-scheduled-activities',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AdminCalendarComponent implements OnInit {
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
    private activityService: ActivityComponentService) {
  }

  ngOnInit() {
    this.service.getScheduledActivities()
      .subscribe(result => {
        for (let item of result) {
          this.events = [
            ...this.events,
            this.createEventFromScheduledActivity(item)
          ];
        }

        this.cd.detectChanges();
      }, error => console.error(error));
  }

  actions: CalendarEventAction[] = [
    {
      label: '<i class="fas fa-fw fa-pencil-alt"></i>',
      a11yLabel: 'Edit',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        if (event.start < new Date()) {
          return;
        }

        this.openModal(event);
      },
    },
    {
      label: '<i class="fas fa-fw fa-trash-alt"></i>',
      a11yLabel: 'Delete',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        if (event.start < new Date()) {
          return;
        }

        this.deleteEvent(event);
      },
    },
  ];

  events: CalendarEvent<ScheduledActivity>[] = [];

  eventTimesChanged({
    event,
    newStart,
    newEnd,
  }: CalendarEventTimesChangedEvent): void {

    const overlappingEvents = this.events.filter((otherEvent) => {
      if (event.meta.id === otherEvent.meta.id) {
        return false;
      }

      return this.intervalsOverlap(newStart, newEnd, otherEvent.start, otherEvent.end);
    });

    if (overlappingEvents.length > 0) {
      return;
    }

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

    this.saveEvent(event);
  }

  intervalsOverlap(start: Date, end: Date, otherStart: Date, otherEnd: Date): boolean {
    return start >= otherStart && start <= otherEnd ||
      end >= otherStart && end <= otherEnd;
  }

  @ViewChild('modalContent', { static: true }) modalContent: TemplateRef<any>;

  modalData: {
    event: CalendarEvent;
    options: {
      availableActivities?: Activity[]
      availableTrainers?: any
    };
  };

  openModal(event: CalendarEvent): void {
    this.activityService.getActivities()
      .subscribe(result => {

        this.modalData = {
          "event": event,
          "options": {
            "availableActivities": result
          }
        };

        if (event.meta && event.meta.trainer) {
          this.modalData.options.availableTrainers = [ event.meta.trainer ];
        }

        this.modal.open(this.modalContent, { size: 'lg' });
      }, error => console.error(error));
  }

  hourSegmentClicked(date: Date) {
    this.selectedDayViewDate = date;

    if (date < new Date()) {
      return;
    }

    let event: CalendarEvent = {
      title: 'New event',
      start: date,
      end: endOfHour(date),
      draggable: true,
      resizable: {
        beforeStart: true,
        afterEnd: true,
      },
    }

    let scheduledActivity: ScheduledActivity = {
      "startTime": event.start,
      "endTime": endOfHour(date),
      "activity": {},
      "trainer": {}
    }

    event.meta = scheduledActivity;

    this.openModal(event);
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

  createEventFromScheduledActivity(scheduledActivity: ScheduledActivity) {
    const event: CalendarEvent = {
      title: scheduledActivity.activity.name,
      start: new Date(scheduledActivity.startTime),
      end: new Date(scheduledActivity.endTime),
      actions: this.actions,
      color: { primary: scheduledActivity.activity.colour, secondary: scheduledActivity.activity.colour },
      draggable: true,
      resizable: {
        beforeStart: true,
        afterEnd: true,
      },
      meta: scheduledActivity
    };

    return event;
  }


  saveEvent(eventToSave: CalendarEvent) {
    if (eventToSave.meta.id) {

      return;
    }


    this.service.saveScheduledActivity(eventToSave.meta)
      .subscribe(result => {
        const event = this.createEventFromScheduledActivity(result);
        this.addEvent(event);
        this.cd.detectChanges();
        this.modal.dismissAll();
      }, error => console.error(error));
  }

  setView(view: CalendarView) {
    this.view = view;
  }

  closeOpenMonthViewDay() {
    this.activeDayIsOpen = false;
  }

  filterTrainers(activityId) {
    const activity = this.modalData.options.availableActivities.find(o => o.id == activityId);

    if (activity === undefined) {
      this.modalData.options.availableTrainers = [];
    } else {
      this.modalData.options.availableTrainers = activity.trainers;
    }

    this.cd.detectChanges();
  }
}
