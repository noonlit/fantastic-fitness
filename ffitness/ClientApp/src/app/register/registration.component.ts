import { Component, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { AuthResponse } from '../auth/auth.model';
import { AuthService } from '../auth/auth.service';
import { Registration } from './registration.model';
import { RegistrationComponentService } from './registration.service';

@Component({
  selector: 'app-registration',
  templateUrl: 'registration.component.html',
  encapsulation: ViewEncapsulation.None,
})
export class RegistrationComponent {
  registrationData = new Registration();
  errorMessages = [];

  constructor(
    private registrationService: RegistrationComponentService,
    private authService: AuthService
  ) { }
  register() {
    if (this.registrationData.birthdateData && this.registrationData.birthdateData.year && this.registrationData.birthdateData.month && this.registrationData.birthdateData.day) {
      this.registrationData.birthdate = this.registrationData.birthdateData.year + '-' + this.registrationData.birthdateData.month + '-' + this.registrationData.birthdateData.day;
    }

    this.registrationService.register(this.registrationData)
      .subscribe(
        () => {
          window.location.href = '/login';
        },
        error => this.errorMessages = error.error.errors
      );
  }
}
