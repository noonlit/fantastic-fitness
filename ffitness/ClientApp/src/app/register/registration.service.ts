import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from '@angular/core';
import { Observable } from "rxjs";
import { Registration } from "./registration.model";

@Injectable({
  providedIn: 'root'
})

export class RegistrationComponentService {
  private apiUrl;

  constructor(private httpClient: HttpClient, @Inject('API_URL') apiUrl: string) {
    this.apiUrl = apiUrl;
  }

  private getApiUrl() {
    return this.apiUrl + 'authentication';
  }

  public register(data: Registration): Observable<any> {
    return this.httpClient.post<any>(this.getApiUrl() + '/register', data);
  }
}
