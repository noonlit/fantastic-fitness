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
          for (let item of result) {
            if (item.isActive) {
              this.currentSubscription = item;
              continue;
            }

            if (item.isPast) {
              this.pastSubscriptions = [... this.pastSubscriptions, item];
              continue;
            }

            if (item.isFuture) {
              this.futureSubscriptions = [... this.futureSubscriptions, item];
              continue;
            }
          }
        },
        error => this.errorMessages = error.error.errors
      );
  }

  deleteSubscription(subscription) {
    this.service.delete(subscription)
      .subscribe(
        () => {
          this.message = "Success!"
          this.errorMessages = [];
          this.futureSubscriptions = this.futureSubscriptions.filter(u => u.id !== subscription.id);
        },
        error => this.errorMessages = error.error.errors
      );
  }
}
