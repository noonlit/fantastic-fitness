import { error } from '@angular/compiler/src/util';
import { OnInit, OnDestroy } from '@angular/core';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
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

  newTrainer: Trainer.TrainerDetails;
  addTrainerForm: FormGroup;

  constructor(private fb: FormBuilder, private trainerService: TrainerComponentService) { }

  ngOnInit(): void {
    this.addTrainerForm = this.fb.group({
      FirstName: [null, Validators.required],
      LastName: [null, Validators.required],
      Description: [null, Validators.required],
      ProfileImage: [null, Validators.required],
    });
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

  onSubmitForm(): void {
    this.newTrainer = this.addTrainerForm.value;
    this.subscription.add(
      this.trainerService.save(this.newTrainer)
        .subscribe(confirmation => {
          this.message = "Success!"
          this.errorMessages = [];
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
