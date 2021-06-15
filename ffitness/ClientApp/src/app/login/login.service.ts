import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from '@angular/core';
import { Observable } from "rxjs";
import { Login } from "./login.model";

@Injectable({
  providedIn: 'root'
})

export class LoginComponentService {
  private apiUrl;

  constructor(private httpClient: HttpClient, @Inject('API_URL') apiUrl: string) {
    this.apiUrl = apiUrl;
  }

  private getApiUrl() {
    return this.apiUrl + 'authentication';
  }

  public login(data: Login): Observable<any> {
    return this.httpClient.post<any>(this.getApiUrl() + '/login', data);
  }
}
