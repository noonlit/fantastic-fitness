import { Component } from '@angular/core';
import { Subscription } from '../subscriptions.model';
import { SubscriptionsService } from '../subscriptions.service';

@Component({
  selector: 'app-admin-subscriptions',
  templateUrl: './admin-subscriptions.component.html',
  styleUrls: ['./admin-subscriptions.component.css']
})
export class AdminSubscriptionsComponent {

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
