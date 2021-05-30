import { Injectable } from '@angular/core';
import { of } from 'rxjs/internal/observable/of';
import { AUTH_TOKEN_LOCAL_STORAGE_KEY } from './auth.model';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  saveToken(token: string): void {
    localStorage.setItem(AUTH_TOKEN_LOCAL_STORAGE_KEY, token);
  }

  getToken(): string {
    return localStorage.getItem(AUTH_TOKEN_LOCAL_STORAGE_KEY);
  }

  removeToken() {
    localStorage.removeItem(AUTH_TOKEN_LOCAL_STORAGE_KEY);
  }

  isAuthenticated() {
    return of(this.getToken() !== null);
  }
}
