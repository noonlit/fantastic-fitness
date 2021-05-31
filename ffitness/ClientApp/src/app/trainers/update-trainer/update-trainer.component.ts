import { OnInit, OnDestroy } from '@angular/core';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Trainer } from '../shared/trainer';
import { TrainerService } from '../shared/trainer.service';


@Component({
  selector: 'app-update-trainer',
  templateUrl: './update-trainer.component.html',
  styleUrls: ['./update-trainer.component.css']
})
export class AdminUpdateTrainerComponent implements OnInit, OnDestroy {

  private subscription: Subscription = new Subscription();

  newTrainer: Trainer.TrainerDetails;
  trainerId: number;
  updateTrainerForm: FormGroup;
  private selectedTrainer: Trainer.TrainerDefault;

  constructor(
    private fb: FormBuilder,
    private trainerService: TrainerService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.initializeUpdateTrainerForm();
    this.getSelectedTrainerId();
    // this.getAvailableTrainer(this.selectedTrainer.id);
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
    this.updateTrainerForm = this.fb.group({
      id: [null],
      firstName: [null, Validators.required],
      lastName: [null, Validators.required],
      description: [null, Validators.required],
      //ProfileImage: [null],
    });
  }

  get id(): FormControl {
    return this.updateTrainerForm.get('id') as FormControl;
  }

  get firstName(): FormControl {
    return this.updateTrainerForm.get('firstName') as FormControl;
  }

  get lastName(): FormControl {
    return this.updateTrainerForm.get('lastName') as FormControl;
  }

  get description(): FormControl {
    return this.updateTrainerForm.get('description') as FormControl;
  }

  getSelectedTrainerId(): void {
    const trainerId = this.route.snapshot.queryParamMap.get('id');
    if (trainerId) {
      this.trainerId = parseInt(trainerId);
      this.getAvailableTrainer(this.trainerId);
    }
  }

  onSubmitForm(): void {
    this.newTrainer = this.updateTrainerForm.value;
    console.log(this.newTrainer);
    this.subscription.add(
      this.trainerService.updateTrainer(this.trainerId, this.newTrainer).subscribe(confirmation => {
        console.log(confirmation);
      })
    );
  }

  getAvailableTrainer(id: number): void {
    this.subscription.add(
      this.trainerService.getTrainer(id).subscribe(trainer => {
        if (trainer) {
          this.selectedTrainer = trainer;
          this.id.setValue(this.selectedTrainer.id);
          this.firstName.setValue(this.selectedTrainer.firstName);
          this.lastName.setValue(this.selectedTrainer.lastName);
          this.description.setValue(this.selectedTrainer.description);
        }
      })
    );
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
