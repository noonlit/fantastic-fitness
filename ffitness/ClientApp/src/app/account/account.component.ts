import { Input, OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { User } from '../users/shared/user.model';
import { UserComponentService } from '../users/shared/user.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {
  @Input() user: User;
  message: string;
  errorMessages: []

  constructor(private userService: UserComponentService) { }

  ngOnInit() {
    this.userService.getCurrentUser()
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

    this.userService.update(this.user)
      .subscribe(
        () => {
          this.message = "Success!"
          this.errorMessages = [];
        },
        error => this.errorMessages = error.error.errors
      );
  }
}
