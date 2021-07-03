import { Component, Inject, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { UserSubscription } from '../shared/usersubscription.model';
import { UserSubscriptionComponentService } from '../shared/usersubscription.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-usersubscriptions',
  templateUrl: './usersubscriptions.component.html',
  styleUrls: ['./usersubscriptions.component.css']
})
export class AdminUserSubscriptionsComponent implements OnInit {
  subscriptions: UserSubscription[] = [];
  message: string;
  errorMessages: [];

  constructor(private service: UserSubscriptionComponentService, private router: Router) {
  }

  ngOnInit() {
    this.service.getUserSubscriptions()
      .subscribe(
        result => {
          for (let item of result) {
            this.subscriptions = [... this.subscriptions, item];
          }
        },
        error => this.errorMessages = error.error.errors
      );
  }

  addSubscription() {
    this.router.navigate(['/manage-subscriptions/add']);
  }

  deleteSubscription(subscription) {
    this.service.delete(subscription)
      .subscribe(
        () => {
          this.message = "Success!"
          this.errorMessages = [];
          this.subscriptions = this.subscriptions.filter(u => u.id !== subscription.id);
        },
        error => this.errorMessages = error.error.errors
      );
  }
}
