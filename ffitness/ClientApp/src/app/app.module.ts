import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ApiAuthorizationModule } from '../api-authorization/api-authorization.module';
import { AuthorizeGuard } from '../api-authorization/authorize.guard';
import { AuthorizeInterceptor } from '../api-authorization/authorize.interceptor';
import { ActivitiesComponent } from './activities/activities.component';
import { CalendarComponent } from './calendar/calendar.component';
// https://github.com/mattlewis92/angular-calendar
import { BookingsStatsComponent } from './bookings/admin/bookings-stats.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { FlatpickrModule } from 'angularx-flatpickr';
import { LayoutModule } from '@angular/cdk/layout';
import { AccountComponent } from './account/account.component';
import { SidebarNavMenuComponent } from './account/sidebar-nav/sidebar-nav-menu.component';
import { AdminCalendarComponent } from './calendar/admin/calendar/calendar.component';
import { AdminAddTrainerComponent } from './trainers/add-trainer.component';
import { AdminSubscriptionsComponent } from './subscriptions/admin/admin-subscriptions.component';
import { UserSubscriptionsComponent } from './subscriptions/user/user-subscriptions.component';
import { SharedSubscriptionsComponent } from './subscriptions/shared/subscriptions.component';
import { NgbdDatepickerPopup } from './subscriptions/user/datepicker/datepicker-popup';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SubscriptionPick } from './subscriptions/user/pick-subscription/pick-subscription.module';
import { SubscriptionActivation } from './subscriptions/user/subscription-activation/subscription-activation.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    ActivitiesComponent,
    CalendarComponent,
    AccountComponent,
    SidebarNavMenuComponent,
    BookingsStatsComponent,
    AdminCalendarComponent,
    AdminAddTrainerComponent,
    AdminSubscriptionsComponent,
    UserSubscriptionsComponent,
    SharedSubscriptionsComponent,
    NgbdDatepickerPopup,
    SubscriptionPick,
    SubscriptionActivation
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ApiAuthorizationModule,    
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] },
      { path: 'activities', component: ActivitiesComponent },
      { path: 'calendar', component: CalendarComponent },
      { path: 'account', component: AccountComponent },
      { path: 'create-calendar', component: AdminCalendarComponent, canActivate: [AuthorizeGuard] },
      { path: 'bookings-stats', component: BookingsStatsComponent, canActivate: [AuthorizeGuard] },
      { path: 'add-trainer', component: AdminAddTrainerComponent },
      { path: 'update-subscriptions', component: AdminSubscriptionsComponent, canActivate: [AuthorizeGuard] },
      { path: 'active-subscription', component: UserSubscriptionsComponent },
      { path: 'subscriptions', component: SharedSubscriptionsComponent }
    ]),
    BrowserAnimationsModule,
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory,
    }),
    NgbModalModule,
    FlatpickrModule.forRoot(),
    LayoutModule,
    BrowserModule, NgbModule,//datepicker
  ],
  exports: [NgbdDatepickerPopup],//datepicker
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
  ],
  bootstrap: [AppComponent, NgbdDatepickerPopup]
})
export class AppModule { }
