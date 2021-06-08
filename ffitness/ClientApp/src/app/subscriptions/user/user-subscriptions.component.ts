import { Component, NgModule } from '@angular/core';
import { Subscription } from '../subscriptions.model';
import { SubscriptionsService } from '../subscriptions.service';
import { NgbdDatepickerPopup } from './date-picker/datepicker-popup';

@Component({
  selector: 'app-user-subscriptions',
  templateUrl: './user-subscriptions.component.html',
  styleUrls: ['./user-subscriptions.component.css']
})
export class UserSubscriptionsComponent {

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
