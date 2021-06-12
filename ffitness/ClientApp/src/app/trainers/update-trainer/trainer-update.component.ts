import { OnInit, OnDestroy, Input } from '@angular/core';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';
import { Subscription } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { Trainer } from '../shared/trainer';
import { TrainerComponentService } from '../shared/trainer.service';
import { Activity } from '../../activities/shared/activity.model';
import { ActivityComponentService } from '../../activities/shared/activity.service';

@Component({
  selector: 'app-trainer-update',
  templateUrl: './trainer-update.component.html',
  styleUrls: ['./trainer-update.component.css']
})
export class AdminTrainerUpdateComponent implements OnInit, OnDestroy {

  existingTrainer: Trainer.TrainerResponse;
  editedTrainer: Trainer.TrainerRequest;
  message: string;
  errorMessages: [];
  activities: Activity[];
  initialActivityIds: number[];
  updatedTrainerForm: FormGroup;
  private subscription: Subscription = new Subscription();

  constructor(
    private fb: FormBuilder,
    private trainerService: TrainerComponentService,
    private route: ActivatedRoute,
    private location: Location,
    private serviceActivity: ActivityComponentService
  ) {}

  ngOnInit(): void {
    this.getTrainer();
    this.getAvailableActivites();
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
    this.updatedTrainerForm = this.fb.group({
      Id: [null, Validators.required],
      FirstName: [null, Validators.required],
      LastName: [null, Validators.required],
      Description: [null, Validators.required],
      ProfileImage: [null, Validators.required],
      Activities: this.fb.array([] , Validators.required)
    })
  }

  getTrainer(): void {
    this.route.params
      .pipe(switchMap((params: Params) => this.trainerService.getTrainer(params['id'])))
      .subscribe(trainer => {
        if (trainer) {
          this.existingTrainer = trainer;

          this.id.setValue(this.existingTrainer.id);
          this.firstName.setValue(this.existingTrainer.firstName);
          this.lastName.setValue(this.existingTrainer.lastName);
          this.description.setValue(this.existingTrainer.description);
          this.initialActivityIds = this.existingTrainer.activities.map(item => {
            this.selectedActivities.push(new FormControl(item));
            return item.id;
          });
        }
      });
  }

  getAvailableActivites(): void {
    this.serviceActivity.getActivities()
      .subscribe(result => {
        this.activities = result;
      }, error => console.error(error));
  }

  get id(): FormControl {
    return this.updatedTrainerForm.get('Id') as FormControl;
  }

  get firstName(): FormControl {
    return this.updatedTrainerForm.get('FirstName') as FormControl;
  }

  get lastName(): FormControl {
    return this.updatedTrainerForm.get('LastName') as FormControl;
  }

  get description(): FormControl {
    return this.updatedTrainerForm.get('Description') as FormControl;
  }

  get selectedActivities(): FormArray {
    return this.updatedTrainerForm.get('Activities') as FormArray;
  }

  onChangeCheckboxState(event, activity: Activity): void {
    if (event.target.checked) {
      this.selectedActivities.push(new FormControl(activity));
    } else {
      this.selectedActivities.controls.forEach((item: FormControl, index: number) => {
        if (item.value.id === activity.id) {
          this.selectedActivities.removeAt(index);
          return;
        }
      });
    }
  }

  onSubmitForm(): void {
    this.editedTrainer = this.updatedTrainerForm.value;
    this.editedTrainer.Activities = this.getUpdatedActivityIdsList();
    this.subscription.add(
      this.trainerService.update(this.existingTrainer.id, this.editedTrainer)
        .subscribe(confirmation => {
          this.message = "Success!"
          this.errorMessages = [];
        },
          error => this.errorMessages = error.error.errors
        )
    );
  }

  private getUpdatedActivityIdsList(): number[] {
    return this.selectedActivities.controls.map(item => {
      this.selectedActivities.push(item);
      return item.value.id;
    });
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
