import { ChangeDetectorRef, OnDestroy, OnInit, QueryList } from '@angular/core';
import { Component, ViewChildren, ViewChild, ElementRef, Renderer2  } from '@angular/core';
import { Trainer } from '../shared/trainer';
import { Subscription } from 'rxjs';
import { TrainerService } from '../shared/trainer.service';

@Component({
  selector: 'app-trainers-list',
  templateUrl: './list-trainers.component.html',
  styleUrls: ['./list-trainers.component.css']
})
export class AdminListTrainersComponent implements OnInit, OnDestroy {

  @ViewChild('trainersParent', {static: false}) domTrainersParent: ElementRef;
  @ViewChildren('trainers') domTrainers: QueryList<any>; 
  private subscription: Subscription = new Subscription();
  public trainersList: Trainer.TrainerDefault[];

  constructor(private trainersService: TrainerService, private cd: ChangeDetectorRef, private renderer: Renderer2) { }

  ngOnInit() {
    this.getTrainers();
  }

  getTrainers(): void {
    this.subscription.add(
      this.trainersService.getTrainers().subscribe(result => {
        this.trainersList = result;
        this.cd.detectChanges();
      }, error => error => console.error(error))
    );
  }

  onDeleteTrainer(trainerId: string): void {
    this.subscription.add(
      this.trainersService.deleteTrainer(trainerId).subscribe(deleteConfirmation => {
        this.removeTrainerFromList(trainerId);
      })
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
