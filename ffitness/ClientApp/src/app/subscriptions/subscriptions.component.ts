import { Component, Inject, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../auth/auth.service';
import { UserSubscription } from '../user-subscriptions/shared/usersubscription.model';
import { UserSubscriptionComponentService } from '../user-subscriptions/shared/usersubscription.service';
import { Subscription } from './shared/subscription.model';
import { SubscriptionComponentService } from './shared/subscription.service';


@Component({
  selector: 'app-subscriptions',
  templateUrl: './subscriptions.component.html',
  styleUrls: ['./subscriptions.component.css']
})
export class SubscriptionsComponent implements OnInit {

  public subscriptions: Subscription[];
  isAuthenticated: Observable<boolean>;
  message: string;
  errorMessages: [];

  constructor(
    private service: SubscriptionComponentService,
    private userSubscriptionService: UserSubscriptionComponentService,
    private authorizeService: AuthService
  ) {
  }

  ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated();

    this.service.getSubscriptions()
      .subscribe(
        result => {
          this.subscriptions = result;
        },
        error => this.errorMessages = error.error.errors
      );
  }

  public startSubscription(subscriptionId: number, startTime: string) {
    var newSubscription = new UserSubscription();
    newSubscription.startTime = startTime;
    newSubscription.subscriptionId = subscriptionId;

    console.log(newSubscription);

    this.userSubscriptionService.createUserSubscription(newSubscription)
      .subscribe(
        result => {
          this.message = "Success! Your subscription ends on " +
            new Date(result.endTime).toLocaleDateString(undefined, { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' }) +
            ".";

          this.errorMessages = [];
          console.log(result);
        },
        error => this.errorMessages = error.error.errors
      );
  }
}
