import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { Trainer } from './trainer';

@Injectable({providedIn: 'root'})
export class TrainerService {

  constructor(private httpClient: HttpClient, @Inject('API_URL') private apiUrl: string) { }

  getTrainers(): Observable<Trainer.TrainerDefault[]> {
    const url: string = this.apiUrl + 'trainers';
    return this.httpClient.get<Trainer.TrainerDefault[]>(url);
  }

  getTrainer(id: number): Observable<Trainer.TrainerDefault> {
    const url: string = this.apiUrl + 'trainers/' + id;
    return this.httpClient.get<Trainer.TrainerDefault>(url);
  }

  addNewTrainer(trainer: Trainer.TrainerDetails): Observable<Trainer.TrainerConfirmation> {
    const url: string = this.apiUrl + 'trainers';
    return this.httpClient.post<Trainer.TrainerConfirmation>(url, trainer);
  }

  updateTrainer(id: number, trainer: Trainer.TrainerDetails): Observable<Trainer.TrainerConfirmation> {
    const url: string = this.apiUrl + 'trainers/' + id;
    return this.httpClient.put<Trainer.TrainerConfirmation>(url, trainer);
  }

  deleteTrainer(id: string): Observable<any> {
    const url: string = this.apiUrl + 'trainers/' + id;
    return this.httpClient.delete<any>(url);
  }
}
