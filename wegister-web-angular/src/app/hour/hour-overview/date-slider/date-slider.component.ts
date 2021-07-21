import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { WorkweekService } from 'src/app/shared/services/workweek.service';

@Component({
  selector: 'app-date-slider',
  templateUrl: './date-slider.component.html',
  styleUrls: ['./date-slider.component.css']
})
export class DateSliderComponent implements OnInit {
  
  buttonDate = "2020"
  years: any[] = [ ];
  selectedYear = 2020;
  selectedType = 1; //1 = month 2 = week
  dateNumbers: any[] = [{detail: 'Alles', number: 0}];
  monthNames = ["Alles", "Januari", "Februari", "Maart", "April", "Mei", "Juni",
    "Juli", "Augustus", "September", "Oktober", "November", "December"
  ];

  @Output() selectedEntry = new EventEmitter<any>();

  constructor(
    private workweekService: WorkweekService
  ) { }

  ngOnInit(): void {
    this.workweekService.getWorkedYears().subscribe(
      entries => {
        entries.forEach( entry =>
          {
            this.years.push(entry.year)
            this.selectedYear = this.years[0]
          }
        );
        this.workweekService.getMonthsPerYear(this.years[0]).subscribe(
          entries => {
              entries.forEach(entry => {
                this.dateNumbers.push({
                  detail: `${this.monthNames[entry.month]}`,
                  number: entry.month
                });
              });
            }
        );
      }
    );
    
  }

  onSelectYear(year){
    this.buttonDate = year
    this.dateNumbers = [{detail: 'Alles', number: 0}]
    this.workweekService.getMonthsPerYear(year).subscribe(
      entries => {
          entries.forEach(entry => {
            this.dateNumbers.push({
              detail: `${this.monthNames[entry.month]}`,
              number: entry.month
            });
          });
        }
    );
    this.selectedYear = year;
    this.selectedType = 1;
  }

  onClickMonth(){
    this.dateNumbers = [{detail: 'Alles', number: 0}]
    this.workweekService.getMonthsPerYear(this.selectedYear).subscribe(
      entries => {
          entries.forEach(entry => {
            this.dateNumbers.push({
              detail: `${this.monthNames[entry.month]}`,
              number: entry.month
            });
          });
        }
    );
    this.selectedType = 1;
  }

  onClickWeek(){
    this.dateNumbers = [{detail: 'Alles', number: 0}]
    this.workweekService.getWeeksPerYear(this.selectedYear).subscribe(
      entries => {
          entries.forEach(entry => {
            this.dateNumbers.push({
              detail: `Week ${entry.weekNumber}`,
              number: entry.weekNumber
            });
          });
        }
    );
    this.selectedType = 2;
  }

  onSelectDateNumber(number){
    if(this.selectedType == 1)
    {
      this.selectedEntry.emit({Year: this.selectedYear, Month: number, Week: 0})
    }
    if(this.selectedType == 2)
    {
      this.selectedEntry.emit({Year: this.selectedYear, Month: 0, Week: number})
    }
  }
}

