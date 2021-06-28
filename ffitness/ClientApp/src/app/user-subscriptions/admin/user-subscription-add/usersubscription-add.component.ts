import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { Subscription } from '../../../subscriptions/shared/subscription.model';
import { SubscriptionComponentService } from '../../../subscriptions/shared/subscription.service';
import { User } from '../../../users/shared/user.model';
import { UserComponentService } from '../../../users/shared/user.service';
import { UserSubscription } from '../../shared/usersubscription.model';
import { UserSubscriptionComponentService } from '../../shared/usersubscription.service';

@Component({
  selector: 'app-usersubscription-add',
  templateUrl: './usersubscription-add.component.html',
})
export class AdminUserSubscriptionAddComponent implements OnInit {
  subscription: UserSubscription = new UserSubscription();
  users: User[];
  subscriptions: Subscription[];
  message: string;
  errorMessages: [];

  constructor(
    private service: UserSubscriptionComponentService,
    private usersService: UserComponentService,
    private subscriptionsService: SubscriptionComponentService,
    private cd: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.usersService.getUsers().subscribe(response => this.users = response);
    this.subscriptionsService.getSubscriptions().subscribe(response => this.subscriptions = response);
  }

  saveSubscription(startTime: string, subscriptionId: number, userId: string) {
    if (!startTime || !subscriptionId || !userId) {
      this.errorMessages = [];
      this.errorMessages['General'] = ["Cannot save data. All fields are required."];
      return;
    }

    this.subscription.startTime = startTime;
    this.subscription.subscriptionId = subscriptionId;
    this.subscription.userId = userId;

    this.service.save(this.subscription)
      .subscribe(
        result => {
          this.message = "Success!";

          this.errorMessages = [];
        },
        error => {
          if (error.error.errors) {
            this.errorMessages = error.error.errors;
            return;
          }

          this.errorMessages = [];
          this.errorMessages['General'] = [error.error];
        }
      );
  }
}
