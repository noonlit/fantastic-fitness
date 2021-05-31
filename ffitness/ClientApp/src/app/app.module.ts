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
import { AdminAddTrainerComponent } from './trainers/add-trainer/add-trainer.component';
import { AdminUpdateTrainerComponent } from './trainers/update-trainer/update-trainer.component';
import { AdminListTrainersComponent } from './trainers/list-trainers/list-trainers.component';
import { AdminCalendarComponent } from './calendar/admin/calendar/calendar.component';

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
    AdminListTrainersComponent,
    AdminAddTrainerComponent,
    AdminUpdateTrainerComponent,
    AdminUsersComponent,
    AdminUserEditComponent,
    AdminUserAddComponent,
    NewLoginComponent,
    RegistrationComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NgbModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'login', component: NewLoginComponent },
      { path: 'register', component: RegistrationComponent },
      { path: 'activities', component: ActivitiesComponent },
      { path: 'calendar', component: CalendarComponent },
      { path: 'account', component: AccountComponent, canActivate: [AuthGuardService] },
      { path: 'create-calendar', component: AdminCalendarComponent, canActivate: [AuthGuardService, AuthRoleGuardService] },
      { path: 'bookings-stats', component: BookingsStatsComponent, canActivate: [AuthGuardService, AuthRoleGuardService] },
      { path: 'list-trainer', component: AdminListTrainersComponent, canActivate: [AuthorizeGuard] },
      { path: 'add-trainer', component: AdminAddTrainerComponent, canActivate: [AuthGuardService, AuthRoleGuardService] },
      { path: 'update-trainer', component: AdminUpdateTrainerComponent, canActivate: [AuthorizeGuard] },
      { path: 'users', component: AdminUsersComponent, canActivate: [AuthGuardService, AuthRoleGuardService] },
      { path: 'user/edit/:id', component: AdminUserEditComponent, canActivate: [AuthGuardService, AuthRoleGuardService] },
      { path: 'user/add', component: AdminUserAddComponent, canActivate: [AuthGuardService, AuthRoleGuardService] }
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
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
