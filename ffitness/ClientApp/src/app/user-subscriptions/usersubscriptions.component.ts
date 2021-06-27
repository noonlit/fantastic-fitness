import { Component, Inject, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../auth/auth.service';
import { UserSubscription } from './shared/usersubscription.model';
import { UserSubscriptionComponentService } from './shared/usersubscription.service';


@Component({
  selector: 'app-usersubscriptions',
  templateUrl: './usersubscriptions.component.html',
  styleUrls: ['./usersubscriptions.component.css']
})
export class UserSubscriptionsComponent implements OnInit {
  isAuthenticated: Observable<boolean>;
  futureSubscriptions: UserSubscription[] = [];
  pastSubscriptions: UserSubscription[] = [];
  currentSubscription: UserSubscription;
  message: string;
  errorMessages: [];

  constructor(private service: UserSubscriptionComponentService, private authorizeService: AuthService) {
  }

  ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated();

    this.service.getCurrentUserSubscriptions()
      .subscribe(
        result => {
          console.log(result);
          for (let item of result) {
            const daysToStart = this.calcDaysLeft(Date.now(), Date.parse(item.startTime));
            const daysToEnd = this.calcDaysLeft(Date.now(), Date.parse(item.endTime));
            const isActive = daysToStart <= 0 && daysToEnd > 0;

            if (isActive) {
              this.currentSubscription = item;
              item.daysLeft = daysToEnd;
              continue;
            }

            if (daysToEnd <= 0) {
              this.pastSubscriptions = [... this.pastSubscriptions, item];
              continue;
            }

            if (daysToStart > 0) {
              this.futureSubscriptions = [... this.futureSubscriptions, item];
              continue;
            }
          }
        },
        error => this.errorMessages = error.error.errors
      );
  }

  calcDaysLeft(from: number, until: number) {
    const oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds

    return Math.round((until - from) / oneDay);
  }
}
