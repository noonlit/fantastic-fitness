import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { of } from 'rxjs/internal/observable/of';
import { User } from '../users/shared/user.model';
import { AUTH_TOKEN_LOCAL_STORAGE_KEY } from './auth.model';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private apiUrl;

  constructor(private httpClient: HttpClient, @Inject('API_URL') apiUrl: string) {
    this.apiUrl = apiUrl;
  }

  private getApiUrl() {
    return this.apiUrl + 'authentication';
  }

  saveToken(token: string): void {
    localStorage.setItem(AUTH_TOKEN_LOCAL_STORAGE_KEY, token);
  }

  getToken(): string {
    return localStorage.getItem(AUTH_TOKEN_LOCAL_STORAGE_KEY);
  }

  removeToken() {
    localStorage.removeItem(AUTH_TOKEN_LOCAL_STORAGE_KEY);
  }

  isAuthenticated(): Observable<boolean> {
    return of(this.getToken() !== null);
  }

  getCurrentUser(): Observable<User> {
    return this.httpClient.get<User>(this.getApiUrl() + '/current');
  }

  isAdmin(): Observable<boolean> {
    let subject = new Subject<boolean>();

    let result = this.httpClient.get<User>(this.getApiUrl() + '/current');

    result.subscribe(
      user => {
        subject.next(user.roles.indexOf("AppAdmin") != -1);
      },
      error => console.log(error)
    );

    return subject;
  }
}
