import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-sidebar-nav-menu',
  templateUrl: './sidebar-nav-menu.component.html',
  styleUrls: ['./sidebar-nav-menu.component.css']
})
export class SidebarNavMenuComponent implements OnInit {
  public isAdmin: Observable<boolean>;

  constructor(
    private authorizeService: AuthService,
  ) {

  }

  ngOnInit() {
    this.isAdmin = this.authorizeService.isAdmin();
  }
}
