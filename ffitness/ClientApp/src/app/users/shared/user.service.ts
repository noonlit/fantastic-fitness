import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from '@angular/core';
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { User } from "./user.model";

@Injectable({
  providedIn: 'root'
})

export class UserComponentService {
  private apiUrl;

  constructor(private httpClient: HttpClient, @Inject('API_URL') apiUrl: string) {
    this.apiUrl = apiUrl;
  }

  private getApiUrl() {
    return this.apiUrl + 'users';
  }

  public getUsers(): Observable<User[]> {
    return this.httpClient.get<User[]>(this.getApiUrl());
  }

  update(user): Observable<User> {
    const url = `${this.getApiUrl()}/${user.id}`;
    return this.httpClient
      .put<User>(url, user);
  }

  delete(user: User): Observable<User> {
    const url = `${this.getApiUrl()}/${user.id}`;
    return this.httpClient
      .delete<User>(url);
  }

  save(email, username, password): Observable<User> {
    const user = { email, username, password };
    return this.httpClient.post<User>(this.getApiUrl(), user);
  }

  public getUser(id: string): Observable<User> {
    return this.getUsers()
      .pipe(
        map(users => users.find(user => user.id === id))
      );
  }
}
