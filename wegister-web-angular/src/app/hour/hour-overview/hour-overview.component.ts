import { Component, OnInit } from '@angular/core';
import { HourService } from 'src/app/shared/services/hour.service';
import { HourRegistration } from 'src/app/shared/models/hourregistration-model';
import { HourRegistrationList } from 'src/app/shared/models/hourregistration-list-model';

@Component({
  selector: 'app-hour-overview',
  templateUrl: './hour-overview.component.html',
  styleUrls: ['./hour-overview.component.css']
})
export class HourOverviewComponent implements OnInit {
  overallTotalMilis = 0;
  hourRegistrationToChange: HourRegistration;

  workedhours: HourRegistration[] = [];
  totalHoursInSeconds = 0;

  constructor(
    private hourService: HourService
  ) { }

  ngOnInit(): void {
    this.getAllHourRegistrations();
  }

  private workweekState = 0;
  private monthState = 0; 
  private employerState = 0;

  getRegisteredHoursOfWeek(weeknumber: number){
    this.workweekState = weeknumber;
    this.getHourRegistrationsByWeekAndEmployerId(this.workweekState, this.employerState);
  }
  
  getRegisteredHoursOfEmployer(employerId: number){
    this.employerState = employerId;
    if(this.workweekState != 0)
      this.getHourRegistrationsByWeekAndEmployerId(this.workweekState, this.employerState);
    if(this.monthState != 0)
      this.getHourRegistrationsByMonthAndEmployerId(this.monthState, this.employerState);
    if(this.workweekState == 0 && this.monthState ==  0 && this.employerState != 0)
      this.getHourRegistrationsByEmployerId(this.employerState);
    if(this.workweekState == 0 && this.monthState ==  0 && this.employerState == 0)
      this.getAllHourRegistrations();
  }

  openRowItem: number;

  onClickUnfold(workedhour){
    var currentRowItem = this.workedhours.findIndex(obj => obj.id == workedhour.id);

    if(this.openRowItem != undefined && currentRowItem != this.openRowItem)
      (this.workedhours[this.openRowItem].details) ? this.workedhours[this.openRowItem].details = false : null;

    this.openRowItem = currentRowItem;

    (!this.workedhours[currentRowItem].details) 
      ? this.workedhours[currentRowItem].details = true
      : this.workedhours[currentRowItem].details = false;
  }

  onClickChangeHour(workedhour){
    this.hourRegistrationToChange = workedhour;
    this.hourRegistrationToChange.employerId = workedhour.employer.id;
  }

  getSelectedEmployer(event){
    this.hourRegistrationToChange.employerId = event;
  }
  getChangedStartTime(event)
  {
    this.hourRegistrationToChange.startTime = event;
  }
  getChangedEndTime(event)
  {
    this.hourRegistrationToChange.endTime = event;
  }
  getChangedRecreation(event){
    this.hourRegistrationToChange.recreation = event;
  }
  getChangedDescription(event){
    this.hourRegistrationToChange.description = event;
  }

  private getHourRegistrationsByWeekAndEmployerId(weeknumber: number, employerId: number){
    this.hourService.getHourRegistrationsByWeekAndEmployerId(weeknumber, employerId).subscribe(
      resp => {
        this.workedhours = resp.hourRegistrations;
        this.totalHoursInSeconds = resp.totalWorkedHoursInSeconds;
      });
  }

  private getHourRegistrationsByMonthAndEmployerId(month: number, employerId: number){
    this.hourService.getHourRegistrationsByMonthAndEmployerId(month, employerId).subscribe(
      resp => {
        this.workedhours = resp.hourRegistrations;
        this.totalHoursInSeconds = resp.totalWorkedHoursInSeconds;
      });
  }

  private getHourRegistrationsByEmployerId(employerId: number){
    this.hourService.getHourRegistrationsByEmployerId(employerId).subscribe(
      resp => {
        this.workedhours = resp.hourRegistrations;
        this.totalHoursInSeconds = resp.totalWorkedHoursInSeconds;
      });
  }

  private getAllHourRegistrations(){
    this.hourService.getHourRegistrations().subscribe(resp => { 
      this.workedhours = resp.hourRegistrations;
      this.totalHoursInSeconds = resp.totalWorkedHoursInSeconds;
    })
  }

  getRegisteredHoursOfEntry(event){
    if(event.Month == 0){
      this.monthState = 0;
      this.workweekState = event.Week;
      this.getHourRegistrationsByWeekAndEmployerId(this.workweekState, this.employerState);
    }
    if(event.Week == 0){
      this.monthState = event.Month;
      this.workweekState = 0;
      this.getHourRegistrationsByMonthAndEmployerId(this.monthState, this.employerState)
    }
  }

  onClickRemove(){
    this.hourService.deleteteRegistration(this.hourRegistrationToChange).subscribe( resp =>
      window.location.reload()
    );
  }

  onClickChangeHourRegistration(){
    let hour: HourRegistration = new HourRegistration();
    
      hour.id = this.hourRegistrationToChange.id,
      hour.startTime = this.hourRegistrationToChange.startTime,
      hour.endTime = this.hourRegistrationToChange.endTime,
      hour.description = this.hourRegistrationToChange.description,
      hour.recreation = this.hourRegistrationToChange.recreation,
      hour.employerId = this.hourRegistrationToChange.employerId.toString()

    this.hourService.updateRegistration(hour).subscribe(resp =>
      window.location.reload()
    );
  }
}
