import { Component, OnInit } from '@angular/core';
import { Subscription } from './shared/subscriptions.model';
import { SubscriptionsComponentService } from './shared/subscriptions.service'

@Component({
  selector: 'app-subscriptions',
  templateUrl: './subscriptions.component.html',
  styleUrls: ['./subscriptions.component.css']
})
export class SubscriptionsComponent implements OnInit {

  public subscriptions: Subscription[];

  constructor(private service: SubscriptionsComponentService) {
  }

  ngOnInit() {
    this.service.getSubscriptions()
      .subscribe(result => {
        this.subscriptions = result;
      }, error => console.error(error));
  }
}
