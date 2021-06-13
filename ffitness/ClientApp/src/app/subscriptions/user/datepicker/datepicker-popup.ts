import {Component} from '@angular/core';
import {NgbDateStruct} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'ngbd-datepicker-popup',
  templateUrl: './datepicker-popup.html',
  styleUrls: ['./datepicker-popup.css']
})
export class NgbdDatepickerPopup {
  subscriptionStartDate: NgbDateStruct;
}
