import { ChangeDetectorRef, Component, Inject, Injectable, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Activity } from '../../shared/activity.model';
import { ActivityComponentService } from '../../shared/activity.service';


@Component({
  selector: 'app-activities',
  templateUrl: './activities.component.html',
  styleUrls: ['./activities.component.css']
})
export class AdminActivitiesComponent implements OnInit {

  public activities: Activity[];

 // constructor(private service: ActivityComponentService, private cd: ChangeDetectorRef, private router: Router) {  }

  constructor(private service: ActivityComponentService, private router: Router) { }

  message: string;
  errorMessages: [];

  ngOnInit() {
    this.getActivities();
  }

  getActivities() {
    this.service.getActivities()
      .subscribe(
        (result) => {
          this.errorMessages = [];
          this.activities = result;
        },
        error => this.errorMessages = error.error.errors
      );
  }

  /*
  addActivity() {
    this.router.navigate(['/activity/add']);
  }

  editActivity(activity: Activity) {
    this.router.navigate(['/activity/edit', activity.id]);
  }
  */

  deleteActivity(activity: Activity) {
    this.service.delete(activity)
      .subscribe(
        () => {
          this.message = "Success!"
          this.errorMessages = [];
          this.activities = this.activities.filter(a => a.id !== activity.id);
        },
        error => this.errorMessages = error.error.errors
      );
  }
}
