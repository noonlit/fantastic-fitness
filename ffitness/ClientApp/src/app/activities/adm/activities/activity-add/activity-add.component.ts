import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { Activity, ACTIVITY_TYPES } from '../../../shared/activity.model';
import { ActivityComponentService } from '../../../shared/activity.service';

@Component({
  selector: 'app-activity-add',
  templateUrl: './activity-add.component.html',
})
export class AdminActivityAddComponent implements OnInit {
  @Input() activity: Activity = new Activity();
  ACTIVITY_TYPES = ACTIVITY_TYPES;
  message: string;
  errorMessages: [];

  constructor(
    private service: ActivityComponentService,
    private cd: ChangeDetectorRef,
  ) { }

  ngOnInit(): void { }

  saveActivity() {
    this.service.save(this.activity)
      .subscribe(
        () => {
          this.message = "Success!"
          this.errorMessages = [];
          this.activity = new Activity();
          this.cd.detectChanges();
        },
        error => this.errorMessages = error.error.errors
      );
  }
}
