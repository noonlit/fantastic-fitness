import { ChangeDetectorRef, OnDestroy, OnInit, QueryList } from '@angular/core';
import { Component, ViewChildren, ViewChild, ElementRef, Renderer2  } from '@angular/core';
import { Trainer } from '../shared/trainer';
import { Subscription } from 'rxjs';
import { TrainerComponentService } from '../shared/trainer.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-trainers-list',
  templateUrl: './trainers-list.component.html',
  styleUrls: ['./trainers-list.component.css']
})
export class AdminTrainersListComponent implements OnInit, OnDestroy {

  @ViewChild('trainersParent', {static: false}) domTrainersParent: ElementRef;
  @ViewChildren('trainers') domTrainers: QueryList<any>; 
  private subscription: Subscription = new Subscription();
  public trainersList: Trainer.TrainerResponse[];

  message: string;
  errorMessages: [];

  constructor(
    private trainersService: TrainerComponentService,
    private cd: ChangeDetectorRef,
    private renderer: Renderer2,
    private router: Router
  ) { }

  ngOnInit() {
    this.getTrainers();
  }

  addTrainer() {
    this.router.navigate(['/trainer/add']);
  }

  editTrainer(trainer: Trainer.TrainerResponse) {
    this.router.navigate(['/trainer/edit', trainer.id]);
  }

  getTrainers(): void {
    this.subscription.add(
      this.trainersService.getTrainers()
        .subscribe(
          result => {
            this.trainersList = result;
            this.errorMessages = [];
          },
          error => error.errorMessages = error.error.errors
      )
    );
  }

  deleteTrainer(trainerId: string): void {
    this.subscription.add(
      this.trainersService.delete(trainerId)
        .subscribe(
          deleteConfirmation => {
            this.removeTrainerFromList(trainerId);
            this.message = "Success!"
            this.errorMessages = [];
          },
          error => this.errorMessages = error.error.errors
        )
    );
  }

  removeTrainerFromList(trainerId): void {
    let trainerToDelete = this.domTrainers.filter(element => {
      if (element.nativeElement.id == trainerId) {
        return element;
      }
    });

    if (trainerToDelete.length) {
      this.renderer.removeChild(this.domTrainersParent.nativeElement, trainerToDelete[0].nativeElement);
    }
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
