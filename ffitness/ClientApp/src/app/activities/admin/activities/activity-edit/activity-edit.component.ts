import { Component, Input, OnInit } from "@angular/core";
import { ActivatedRoute, Params } from "@angular/router";
import { switchMap } from "rxjs/operators";
import { Activity } from "../../../shared/activity.model";
import { ActivityComponentService } from "../../../shared/activity.service";

@Component({
  selector: 'app-activity-edit',
  templateUrl: './activity-edit.component.html',
})
export class AdminActivityEditComponent implements OnInit {
  @Input() activity: Activity;
  message: string;
  errorMessages: []

  constructor(private activityService: ActivityComponentService,
    private route: ActivatedRoute,
    private location: Location) {
  }

  ngOnInit(): void {
    this.route.params
      .pipe(switchMap((params: Params) => this.activityService.getActivity(params['id'])))
      .subscribe(activity => {
        this.activity = activity
      });
  }

  save(): void {
  this.activityService.update(this.activity)
      .subscribe(
        () => {
          this.message = "Success!" 
          this.errorMessages = [];
        },
        error => this.errorMessages = error.error.errors
      );
  }

  /*
  goBack(): void {
    this.location.back();
  }
  */
}
