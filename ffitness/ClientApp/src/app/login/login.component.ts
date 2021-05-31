import { Component, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { AuthResponse } from '../auth/auth.model';
import { AuthService } from '../auth/auth.service';
import { Login } from './login.model';
import { LoginComponentService } from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: 'login.component.html',
  encapsulation: ViewEncapsulation.None,
})
export class NewLoginComponent {
  loginData = new Login();
  errorMessages = [];

  constructor(
    private loginService: LoginComponentService,
    private authService: AuthService
  ) {}
  logIn() {
    this.loginService.login(this.loginData)
      .subscribe(
        (response: AuthResponse) => {
          this.authService.saveToken(response.token);
          window.location.href = '/';
        },
        error => this.errorMessages = error.error.errors
      );
  }
}
