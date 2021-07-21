import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { HourRegisterService } from 'src/app/hour-register/hour-register.service';

@Component({
  selector: 'app-week-selector',
  templateUrl: './week-selector.component.html',
  styleUrls: ['./week-selector.component.css']
})
export class WeekSelectorComponent implements OnInit {
  buttonWorkweekText = "Werkweek"
  weekDetails = [ ];

  monthNames = ["January", "February", "March", "April", "May", "June",
  "July", "August", "September", "October", "November", "December"
  ];

  @Input() firstWorkedDate: Date;
  @Output() selectedWeeknumber = new EventEmitter<number>();
  
  constructor(
    private hourRegisterService: HourRegisterService
  ) { }

  ngOnInit(): void {
    this.hourRegisterService.getWeeknumbers().subscribe(weeks => { 
      this.weekDetails.push({
        weeknumber: 0,
        weekdetail: "All"
      });
      weeks.forEach(element => {
        var startdate = new Date(element.startDate)
        var enddate = new Date(element.endDate)

        this.weekDetails.push({
          weeknumber: element.weekNumber,
          weekdetail: `Week ${element.weekNumber}: ${startdate.getDate()} ${this.monthNames[startdate.getMonth()]} ${startdate.getFullYear()} - ${enddate.getDate()} ${this.monthNames[enddate.getMonth()]} ${enddate.getFullYear()}`
        });
      });
    });
  }

  selectWeek(workweek: any){
    console.log(workweek)
    this.selectedWeeknumber.emit(workweek.weeknumber);
    if(workweek.weeknumber == 0){
      this.buttonWorkweekText = `All`;
    }
    else{
      this.buttonWorkweekText = `Week ${workweek.weeknumber}`;
    }
  }
}
