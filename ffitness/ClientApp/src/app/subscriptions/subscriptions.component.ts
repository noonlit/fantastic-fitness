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
  public newSubscriptionStartDate;

  constructor(
    private service: SubscriptionComponentService,
    private userSubscriptionService: UserSubscriptionComponentService,
    private authorizeService: AuthService
  ) {
  }

  ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated();

    this.service.getSubscriptions()
      .subscribe(result => {
        this.subscriptions = result;
      }, error => console.error(error));
  }

  public startSubscription(subscriptionId: number, startTime) {
    var newSubscription = new UserSubscription();
    newSubscription.startTime = new Date(startTime);
    newSubscription.subscriptionId = subscriptionId;

    this.userSubscriptionService.createUserSubscription(newSubscription)
      .subscribe(result => {
        console.log(result);
      });
  }
}
