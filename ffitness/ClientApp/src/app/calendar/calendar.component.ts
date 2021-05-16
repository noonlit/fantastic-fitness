import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ScheduledActivity } from './calendar.model';

@Component({
  selector: 'app-scheduled-activities',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent {

  public activities: ScheduledActivity[];

  constructor(http: HttpClient, @Inject('API_URL') apiUrl: string) {
    http.get<ScheduledActivity[]>(apiUrl + 'scheduledactivities').subscribe(result => {
      this.activities = result;
    }, error => console.error(error));
  }


  ngOnInit() {
  }
}
