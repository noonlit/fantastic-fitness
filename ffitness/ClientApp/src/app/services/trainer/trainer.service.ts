import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { Trainer } from '../../types/trainer';

@Injectable({providedIn: 'root'})
export class TrainerService {

  constructor(private http: HttpClient, @Inject('API_URL') private apiUrl: string) { }

  getTrainers(): Observable<Trainer.TrainerDefault[]> {
    const url: string = this.apiUrl + 'trainers';
    return this.http.get<Trainer.TrainerDefault[]>(url);
  }

  addNewTrainer(trainer: Trainer.TrainerDetails): Observable<Trainer.TrainerConfirmation> {
    const url: string = this.apiUrl + 'trainers';
    return this.http.post<Trainer.TrainerConfirmation>(url, trainer);
  }
}
