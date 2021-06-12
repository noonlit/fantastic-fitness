import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { Activity } from '../../activities/shared/activity.model';
import { Trainer } from './trainer';

@Injectable({providedIn: 'root'})
export class TrainerComponentService {

  constructor(private httpClient: HttpClient, @Inject('API_URL') private apiUrl: string) { }

  getTrainers(): Observable<Trainer.TrainerResponse[]> {
    const url: string = this.getApiUrl();
    return this.httpClient.get<Trainer.TrainerResponse[]>(url);
  }

  getTrainer(id: number): Observable<Trainer.TrainerResponse> {
    const url: string = this.getApiUrl() + '/' + id;
    return this.httpClient.get<Trainer.TrainerResponse>(url);
  }

  getActivities(): Observable<Activity> {
    const url: string = this.apiUrl + 'activities';
    return this.httpClient.get<Activity>(url);
  }

  save(trainer: Trainer.TrainerRequest): Observable<Trainer.TrainerConfirmation> {
    const url: string = this.getApiUrl();
    return this.httpClient.post<Trainer.TrainerConfirmation>(url, trainer);
  }

  update(id: number, trainer: Trainer.TrainerRequest): Observable<Trainer.TrainerConfirmation> {
    const url: string = this.getApiUrl() + '/' + id;
    return this.httpClient.put<Trainer.TrainerConfirmation>(url, trainer);
  }

  delete(id: string): Observable<any> {
    const url: string = this.getApiUrl() +'/' + id;
    return this.httpClient.delete<any>(url);
  }

  private getApiUrl() {
    return this.apiUrl + 'trainers';
  }
}
