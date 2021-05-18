import { Component, Inject, OnInit } from '@angular/core';
import { Activity } from './shared/activity.model';
import { ActivityComponentService } from './shared/activity.service';


@Component({
  selector: 'app-activities',
  templateUrl: './activities.component.html',
  styleUrls: ['./activities.component.css']
})
export class ActivitiesComponent implements OnInit {

  public activities: Activity[];

  constructor(private service: ActivityComponentService) {
  }

  ngOnInit() {
    this.service.getActivities()
      .subscribe(result => {
        this.activities = result;
      }, error => console.error(error));
  }
}
