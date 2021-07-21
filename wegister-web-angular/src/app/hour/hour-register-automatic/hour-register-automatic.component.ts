import { Component, OnInit } from '@angular/core';
import { HourService } from '../../shared/services/hour.service';
import { EmployerService } from 'src/app/employer/employer.service';
import { HourRegistration } from 'src/app/shared/models/hourregistration-model';
import { Employer } from 'src/app/models/employer.model';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-hour-register-automatic',
  templateUrl: './hour-register-automatic.component.html',
  styleUrls: ['./hour-register-automatic.component.css']
})
export class HourRegisterAutomaticComponent implements OnInit {
  today = new Date();
  activeRegistration: HourRegistration;
  totalAmountOfMinutesWorked = 0;
  recreation = 0;
  total = 'zonderberekening';
  hourRegistrationForm: FormGroup;
  
  constructor(
    private formBuilder: FormBuilder,
    private hourService: HourService
  ) {
    this.hourRegistrationForm = this.formBuilder.group({
      recreation: "",
      description: "",
      employerId: ""
    });
   }

  ngOnInit(): void {
    this.hourService.getActiveRegistration().subscribe(hourregistration => {
      this.activeRegistration = hourregistration
    });
    this.today.setHours(this.today.getHours()+2)
  }

  calculateTotalWorkedHours(startdate: Date, enddate: Date){
    var hours = (enddate.getHours() - startdate.getHours()) * 60;
    var minutes = enddate.getMinutes() - startdate.getMinutes();
    minutes = minutes - this.recreation;
    return hours + minutes;
  }

  onCalculate(){
    this.totalAmountOfMinutesWorked = this.calculateTotalWorkedHours(new Date(this.activeRegistration.startTime), this.today);
    this.total = this.getTotalWorked();
  }

  getTotalWorked(){
    var minutes =  ((this.totalAmountOfMinutesWorked / 60) * 10 % 10 /10);
    var hours = (this.totalAmountOfMinutesWorked / 60) - minutes;
    
    var minutesFormat = Math.round(minutes*60).toString();
    var hoursFormat = Math.round(hours-2).toString(); 
    
    if(minutesFormat.length == 1)
      minutesFormat = `0${minutesFormat}`
    if(hoursFormat.length == 1)
    hoursFormat = `0${hoursFormat}`

    return `${hoursFormat}:${minutesFormat}`
  }

  onRecreationAdd(event){
    this.activeRegistration.recreation = event;
    this.recreation = event;
    this.onCalculate();
    this.total = this.getTotalWorked();
  }

  onClickStartRegistration(){
    var registration: HourRegistration = new HourRegistration();
    registration.startTime = this.today;
    this.hourService.startAutomaticRegistration(registration).subscribe(response => {
      window.location.reload();
    });
  }

  onRegisterHours(){
    let hour = new HourRegistration(this.hourRegistrationForm.value);
    hour.id = this.activeRegistration.id;
    hour.startTime = this.activeRegistration.startTime;
    hour.endTime = this.today;

    if(hour.employerId === undefined){
      window.alert('Selecteer een werkgever')
    }
    else{
      this.hourService.stopAutomaticRegistration(hour).subscribe(response => {
        window.location.reload();
      });
    }
  }
}
