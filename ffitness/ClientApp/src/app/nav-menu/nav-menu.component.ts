import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  public isAuthenticated: Observable<boolean>;

  constructor(
    private authorizeService: AuthService,
  ) {

  }

  ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
  }

  logout() {
    this.authorizeService.removeToken();
    window.location.href = '/';
  }
}
