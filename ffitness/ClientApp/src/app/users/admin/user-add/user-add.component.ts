import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { User } from '../../shared/user.model';
import { UserComponentService } from '../../shared/user.service';

@Component({
  selector: 'app-user-add',
  templateUrl: './user-add.component.html',
})
export class AdminUserAddComponent implements OnInit {
  @Input() user: User = new User();
  message: string;
  errorMessages = [];

  constructor(
    private service: UserComponentService,
    private cd: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
  }

  saveUser() {
    this.errorMessages = [];

    this.service.save(this.user)
      .subscribe(
        () => {
          this.message = "Success!"
          this.errorMessages = [];
          this.user = new User();
          this.cd.detectChanges();
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
}
