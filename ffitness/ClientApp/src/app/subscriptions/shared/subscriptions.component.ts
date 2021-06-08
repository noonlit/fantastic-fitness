import { Component } from '@angular/core';
import { Subscription } from '../subscriptions.model';
import { SubscriptionsService } from '../subscriptions.service';

@Component({
  selector: 'app-shared-subscriptions',
  templateUrl: './subscriptions.component.html',
  styleUrls: ['./subscriptions.component.css']
})
export class SharedSubscriptionsComponent {

  public subscriptions: Subscription[];

  constructor(private subscriptionsService: SubscriptionsService) {

  }

  getSubscriptions() {
    this.subscriptionsService.getSubscriptions().subscribe(p => this.subscriptions = p);
  }

  ngOnInit() {
    this.getSubscriptions();
  }

}
