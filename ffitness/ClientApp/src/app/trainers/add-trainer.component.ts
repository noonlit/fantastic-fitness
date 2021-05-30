import { OnInit, OnDestroy } from '@angular/core';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Trainer } from './shared/trainer';
import { TrainerService } from './shared/trainer.service';


@Component({
  selector: 'app-add-trainer',
  templateUrl: './add-trainer.component.html',
  styleUrls: ['./add-trainer.component.css']
})
export class AdminAddTrainerComponent implements OnInit, OnDestroy {

  private subscription: Subscription = new Subscription();

  newTrainer: Trainer.TrainerDetails;
  addTrainerForm: FormGroup;

  constructor(private fb: FormBuilder, private trainerService: TrainerService) { }

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
    console.log(this.newTrainer);
    this.subscription.add(
      this.trainerService.addNewTrainer(this.newTrainer).subscribe(confirmation => {
        console.log(confirmation);
      })
    );
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
