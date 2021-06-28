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
import { AdminUsersComponent } from './users/admin/users.component';
import { AdminUserEditComponent } from './users/admin/user-edit/user-edit.component';
import { AdminUserAddComponent } from './users/admin/user-add/user-add.component';
import { NewLoginComponent } from './login/login.component';
import { AuthService } from './auth/auth.service';
import { TokenInterceptor } from './auth/auth.token.interceptor';
import { AuthGuardService } from './auth/auth.guard';
import { AuthRoleGuardService } from './auth/auth.role.guard';
import { RegistrationComponent } from './register/registration.component';
import { NgbDate, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AdminTrainerAddComponent } from './trainers/add-trainer/trainer-add.component';
import { AdminTrainerUpdateComponent } from './trainers/update-trainer/trainer-update.component';
import { AdminTrainersListComponent } from './trainers/list-trainers/trainers-list.component';
import { AdminCalendarComponent } from './calendar/admin/calendar/calendar.component';
import { AdminActivitiesComponent } from './activities/admin/activities/activities.component';
import { ChartsModule, ThemeService } from 'ng2-charts';
import { AdminActivityAddComponent } from './activities/admin/activities/activity-add/activity-add.component';
import { AdminActivityEditComponent } from './activities/admin/activities/activity-edit/activity-edit.component';
import { SubscriptionsComponent } from './subscriptions/subscriptions.component';
import { UserSubscriptionsComponent } from './user-subscriptions/usersubscriptions.component';
import { AdminUserSubscriptionsComponent } from './user-subscriptions/admin/usersubscriptions.component';
import { AdminUserSubscriptionAddComponent } from './user-subscriptions/admin/user-subscription-add/usersubscription-add.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ActivitiesComponent,
    CalendarComponent,
    AccountComponent,
    SidebarNavMenuComponent,
    BookingsStatsComponent,
    AdminCalendarComponent,
    AdminTrainersListComponent,
    AdminTrainerAddComponent,
    AdminTrainerUpdateComponent,
    AdminUsersComponent,
    AdminUserEditComponent,
    AdminUserAddComponent,
    AdminActivitiesComponent,
    AdminActivityAddComponent,
    AdminActivityEditComponent,
    NewLoginComponent,
    RegistrationComponent,
    SubscriptionsComponent,
    UserSubscriptionsComponent,
    AdminUserSubscriptionsComponent,
    AdminUserSubscriptionAddComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NgbModule,
    ReactiveFormsModule,
    ChartsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'login', component: NewLoginComponent },
      { path: 'register', component: RegistrationComponent },
      { path: 'activities', component: ActivitiesComponent },
      { path: 'calendar', component: CalendarComponent },
      { path: 'account', component: AccountComponent, canActivate: [AuthGuardService] },
      { path: 'create-calendar', component: AdminCalendarComponent, canActivate: [AuthGuardService, AuthRoleGuardService] },
      { path: 'bookings-stats', component: BookingsStatsComponent, canActivate: [AuthGuardService, AuthRoleGuardService] },
      { path: 'trainers', component: AdminTrainersListComponent, canActivate: [AuthGuardService] },
      { path: 'trainer/add', component: AdminTrainerAddComponent, canActivate: [AuthGuardService, AuthRoleGuardService] },
      { path: 'trainer/edit/:id', component: AdminTrainerUpdateComponent, canActivate: [AuthGuardService] },
      { path: 'users', component: AdminUsersComponent, canActivate: [AuthGuardService, AuthRoleGuardService] },
      { path: 'user/edit/:id', component: AdminUserEditComponent, canActivate: [AuthGuardService, AuthRoleGuardService] },
      { path: 'user/add', component: AdminUserAddComponent, canActivate: [AuthGuardService, AuthRoleGuardService] },
      { path: 'activity/add', component: AdminActivityAddComponent, canActivate: [AuthGuardService, AuthRoleGuardService] },
      { path: 'manage-activities', component: AdminActivitiesComponent, canActivate: [AuthGuardService, AuthRoleGuardService] },
      { path: 'activity/edit/:id', component: AdminActivityEditComponent, canActivate: [AuthGuardService, AuthRoleGuardService] }
      { path: 'manage-activities', component: AdminActivitiesComponent, canActivate: [AuthGuardService, AuthRoleGuardService] },
      { path: 'subscriptions', component: SubscriptionsComponent },
      { path: 'user-subscriptions', component: UserSubscriptionsComponent },
      { path: 'manage-subscriptions', component: AdminUserSubscriptionsComponent, canActivate: [AuthGuardService, AuthRoleGuardService]},
      { path: 'manage-subscriptions/add', component: AdminUserSubscriptionAddComponent, canActivate: [AuthGuardService, AuthRoleGuardService] }
    ]),
    BrowserAnimationsModule,
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory,
    }),
    NgbModalModule,
    FlatpickrModule.forRoot(),
    LayoutModule
  ],
  providers: [
    AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    },
    ThemeService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
