import { OnInit, OnDestroy, Input } from '@angular/core';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';
import { Subscription } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { Trainer } from '../shared/trainer';
import { TrainerComponentService } from '../shared/trainer.service';


@Component({
  selector: 'app-trainer-update',
  templateUrl: './trainer-update.component.html',
  styleUrls: ['./trainer-update.component.css']
})
export class AdminTrainerUpdateComponent implements OnInit, OnDestroy {

  @Input() trainer: Trainer.TrainerDefault;
  message: string;
  errorMessages: []

  private subscription: Subscription = new Subscription();

  constructor(
    private fb: FormBuilder,
    private trainerService: TrainerComponentService,
    private route: ActivatedRoute,
    private location: Location
  ) {}

  ngOnInit(): void {
    this.initializeUpdateTrainerForm();
  }

/*  onFileChange(event): void {
    const reader: FileReader = new FileReader();

    if (event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      reader.readAsDataURL(file);

      reader.onload = () => {
        this.updateTrainerForm.patchValue({
          ProfileImage: reader.result
        });
      }
    }
  }*/

  initializeUpdateTrainerForm(): void {
    this.route.params
      .pipe(switchMap((params: Params) => this.trainerService.getTrainer(params['id'])))
      .subscribe(trainer => {
        this.trainer = trainer
      });
  }

  save(): void {
    this.trainerService.update(this.trainer.id, this.trainer)
      .subscribe(
        () => {
          this.message = "Success!"
          this.errorMessages = [];
        },
        error => this.errorMessages = error.error.errors
      );
  }

  goBack(): void {
    this.location.back();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
