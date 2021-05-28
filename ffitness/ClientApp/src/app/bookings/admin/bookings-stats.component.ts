import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-bookings-stats',
  templateUrl: './bookings-stats.component.html',
  styleUrls: ['./bookings-stats.component.css']
})
export class BookingsStatsComponent implements OnInit {
  constructor() { }

  ngOnInit() {
    
  }
}
