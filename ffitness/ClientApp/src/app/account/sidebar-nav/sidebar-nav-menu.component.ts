import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs/operators';
import { AuthorizeService } from '../../../api-authorization/authorize.service';

@Component({
  selector: 'app-sidebar-nav-menu',
  templateUrl: './sidebar-nav-menu.component.html',
  styleUrls: ['./sidebar-nav-menu.component.css']
})
export class SidebarNavMenuComponent implements OnInit {
  public isAuthenticated: Observable<boolean>;
  public userName: Observable<string>;

  constructor(private authorizeService: AuthorizeService) { }

  ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.userName = this.authorizeService.getUser().pipe(map(u => u && u.name)); // TODO - need role - isAdmin
  }
}
