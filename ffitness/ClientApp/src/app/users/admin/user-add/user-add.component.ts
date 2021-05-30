import { Component, OnInit } from '@angular/core';
import { UserComponentService } from '../../shared/user.service';

@Component({
  selector: 'app-user-add',
  templateUrl: './user-add.component.html',
})
export class AdminUserAddComponent implements OnInit {
  message: string;
  errorMessages: [];

  constructor(private service: UserComponentService) { }

  ngOnInit(): void {
  }

  saveUser(email, username, password) {
    this.service.save(email, username, password)
      .subscribe(
        () => {
          this.message = "Success!"
          this.errorMessages = [];
        },
        error => this.errorMessages = error.error.errors
      );
  }
}
