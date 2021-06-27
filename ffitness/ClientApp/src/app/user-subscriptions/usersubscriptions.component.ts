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

  constructor(private service: UserSubscriptionComponentService, private authorizeService: AuthService) {
  }

  ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
  }
}
