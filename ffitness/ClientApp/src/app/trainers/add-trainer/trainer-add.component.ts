import { error } from '@angular/compiler/src/util';
import { OnInit, OnDestroy } from '@angular/core';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Activity } from '../../activities/shared/activity.model';
import { ActivityComponentService } from '../../activities/shared/activity.service';
import { Trainer } from '../shared/trainer';
import { TrainerComponentService } from '../shared/trainer.service';

@Component({
  selector: 'app-trainer-add',
  templateUrl: './trainer-add.component.html',
  styleUrls: ['./trainer-add.component.css']
})
export class AdminTrainerAddComponent implements OnInit, OnDestroy {

  message: string;
  errorMessages: [];

  private subscription: Subscription = new Subscription();

  newTrainer: Trainer.TrainerRequest;
  addTrainerForm: FormGroup;
  activities: Activity[];

  constructor(
    private fb: FormBuilder,
    private trainerService: TrainerComponentService,
    private serviceActivity: ActivityComponentService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.serviceActivity.getActivities()
      .subscribe(result => {
        this.activities = result;
      }, error => console.error(error));

    this.initForm();
  }

  initForm() {
    this.addTrainerForm = this.fb.group({
      FirstName: [null, Validators.required],
      LastName: [null, Validators.required],
      Description: [null, Validators.required],
      ProfileImage: [null, Validators.required],
      Activities: this.fb.array([], Validators.required)
    });
  }

  get selectedActivities(): FormArray {
    return this.addTrainerForm.get('Activities') as FormArray;
  }

  onFileChange(event): void {
    const reader: FileReader = new FileReader();

    if (event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      reader.readAsDataURL(file);

      reader.onload = () => {
        this.addTrainerForm.patchValue({
          ProfileImage: reader.result
        });
      }
    }
  }

  onChangeCheckboxState(event, activity: Number): void {
    if (event.target.checked) {
      this.selectedActivities.push(new FormControl(activity));
    } else {
      this.selectedActivities.controls.forEach((item: FormControl, index: number) => {
        if (item.value.id === activity) {
          this.selectedActivities.removeAt(index);
          return;
        }
      });
    }
  }

  onSubmitForm(): void {
    this.newTrainer = this.addTrainerForm.value;
    this.subscription.add(
      this.trainerService.save(this.newTrainer)
        .subscribe(confirmation => {
          this.message = "Success!"
          this.errorMessages = [];
          this.initForm();
          this.router.navigate(['/trainers']);
        },
          error => this.errorMessages = error.error.errors     
      )
    );
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
/*const checkedResults = Object.keys(this.addTrainerForm.value).filter(prop => {
      if(this.addTrainerForm.value[prop] !== null) {
        return this.addTrainerForm.value[prop]
      }
    })
    console.log(checkedResults);*/
