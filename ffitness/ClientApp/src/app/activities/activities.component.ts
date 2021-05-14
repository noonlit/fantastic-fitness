import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Activity } from './activity.model';

@Component({
  selector: 'app-activities',
  templateUrl: './activities.component.html',
  styleUrls: ['./activities.component.css']
})
export class ActivitiesComponent {

  public activities: Activity[];

  constructor(http: HttpClient, @Inject('API_URL') apiUrl: string) {
    http.get<Activity[]>(apiUrl + 'activities').subscribe(result => {
      this.activities = result;
    }, error => console.error(error));
  }


  ngOnInit() {
  }

}
