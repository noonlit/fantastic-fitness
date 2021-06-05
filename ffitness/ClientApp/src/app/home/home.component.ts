import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { Trainer } from '../trainers/shared/trainer';
import { TrainerComponentService } from '../trainers/shared/trainer.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  private subscription: Subscription = new Subscription();

  trainers: Trainer.TrainerDefault[] = null;

  constructor(private trainerService: TrainerComponentService) {}

  ngOnInit(): void {
    this.getAvailableTrainers();
  }

  getAvailableTrainers(): void {
    this.subscription.add(
      this.trainerService.getTrainers().subscribe(trainers => {
        this.trainers = trainers;
      })
    );
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
