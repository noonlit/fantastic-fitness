import { ChangeDetectorRef, OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs/operators';
import { BookingStatsComponentService } from '../shared/booking-stats.service';

@Component({
  selector: 'app-bookings-stats',
  templateUrl: './bookings-stats.component.html',
  styleUrls: ['./bookings-stats.component.css']
})
export class BookingsStatsComponent implements OnInit {
  constructor(
    private bookingStatsService: BookingStatsComponentService,
    private cd: ChangeDetectorRef
  ) { }

  public bookedScheduledActivitiesStats = [];
  public popularActivitiesStats = [];
  public popularTrainersStats = [];

  public colours = [
    {
      backgroundColor: 'rgba(104,176,171,0.2)'
    }
  ]

  public chartOptions = {
    responsive: true,
    indexAxis: "y",
    scales: {
      yAxes: [{
        ticks: {
          beginAtZero: true,
          min: 0,
          max: 100,
        }
      }]
    }
  }

  ngOnInit() {
    this.bookingStatsService.getBookedScheduledActivitiesStats().subscribe(result => {
      this.bookedScheduledActivitiesStats = result;
      this.cd.detectChanges();
    }, error => error => console.error(error));

    this.bookingStatsService.getPopularTrainersStats().subscribe(result => {
      this.popularTrainersStats = result;
      this.displayTrainersStats();
      this.cd.detectChanges();
    }, error => error => console.error(error));

    this.bookingStatsService.getPopularActivitiesStats().subscribe(result => {
      this.popularActivitiesStats = result;
      this.displayActivitiesStats();
      this.cd.detectChanges();
    }, error => error => console.error(error));
  }

  activitiesChartData = [
    {
      label: 'Occupancy (%)',
      data: []
    }
  ];

  activitiesChartLabels = [];

  displayActivitiesStats() {
    for (let item of this.popularActivitiesStats) {
      this.activitiesChartData[0].data.push(parseInt(item.occupancyPercentage));
      this.activitiesChartLabels.push(item.activityName);
    }
  }

  trainersChartData = [
    {
      label: 'Occupancy (%)',
      data: []
    }
  ];

  trainersChartLabels = [];

  displayTrainersStats() {
    for (let item of this.popularTrainersStats) {
      this.trainersChartData[0].data.push(parseInt(item.occupancyPercentage));
      this.trainersChartLabels.push(item.trainerName);
    }
  }
}
