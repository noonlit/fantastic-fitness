import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from './../shared/user.model';
import { UserComponentService } from './../shared/user.service';


@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
})
export class AdminUsersComponent implements OnInit {

  public users: User[];

  constructor(
    private service: UserComponentService,
    private cd: ChangeDetectorRef,
    private router: Router
  ) {
  }

  message: string;
  errorMessages: [];

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
    this.service.getUsers()
      .subscribe(
        (result) => {
          this.errorMessages = [];
          this.users = result;
        },
        error => this.errorMessages = error.error.errors
      );
  }

  editUser(user: User) {
    this.router.navigate(['/user/edit', user.id]);
  }

  deleteUser(user: User) {
    this.service.delete(user)
      .subscribe(
        () => {
          this.message = "Success!"
          this.errorMessages = [];
          this.users = this.users.filter(u => u.id !== user.id);
        },
        error => this.errorMessages = error.error.errors
      );
  }

  userIsAdmin(user: User): boolean {
    return user.roles.indexOf('AppAdmin') !== -1;
  }

  makeAdmin(user: User) {
    // todo
  }

  addUser() {
    this.router.navigate(['/user/add']);
  }
}
