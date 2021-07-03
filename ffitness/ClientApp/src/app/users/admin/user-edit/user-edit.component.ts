import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from "@angular/router";
import { Location } from '@angular/common';
import { switchMap } from "rxjs/operators";
import { UserComponentService } from '../../shared/user.service';
import { User } from '../../shared/user.model';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
})
export class AdminUserEditComponent implements OnInit {
  @Input() user: User;
  message: string;
  errorMessages = [];

  constructor(private userService: UserComponentService,
    private route: ActivatedRoute,
    private location: Location) {
  }

  ngOnInit(): void {
    this.route.params
      .pipe(switchMap((params: Params) => this.userService.getUser(params['id'])))
      .subscribe(user => {
        user.birthdateData = {};
        var birthdayAsDate = new Date(user.birthDate);
        user.birthdateData.year = birthdayAsDate.getFullYear();
        user.birthdateData.month = birthdayAsDate.getMonth() + 1;
        user.birthdateData.day = birthdayAsDate.getDate();

        this.user = user
      });
  }

  save(): void {
    this.user.birthDate = this.user.birthdateData.year + '-' + this.user.birthdateData.month + '-' + this.user.birthdateData.day;
    this.errorMessages = [];

    this.userService.update(this.user)
      .subscribe(
        () => {
          this.message = "Success!"
          this.errorMessages = [];
        },
        error => {
          if (error.error.errors) {
            this.errorMessages = error.error.errors;
            return;
          }

          if (!error.error) {
            return;
          }

          for (const element of error.error) {
            this.errorMessages[element.code] = [];
            this.errorMessages[element.code].push(element.description);
          }
        }
      );
  }

  goBack(): void {
    this.location.back();
  }
}
