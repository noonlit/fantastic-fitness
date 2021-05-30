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

  constructor(private userService: UserComponentService,
    private route: ActivatedRoute,
    private location: Location) {
  }

  ngOnInit(): void {
    this.route.params
      .pipe(switchMap((params: Params) => this.userService.getUser(params['id'])))
      .subscribe(user => this.user = user);
  }

  save(): void {
    this.userService.update(this.user)
      .subscribe(() => this.message = "Success!");
  }

  goBack(): void {
    this.location.back();
  }
}
