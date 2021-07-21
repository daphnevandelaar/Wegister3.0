import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HourService } from 'src/app/shared/services/hour.service';
import { Router } from '@angular/router';
import { HourRegistration } from 'src/app/shared/models/hourregistration-model';

@Component({
  selector: 'app-hour-register',
  templateUrl: './hour-register.component.html',
  styleUrls: ['./hour-register.component.css']
})
export class HourRegisterComponent {

  hourRegistrationForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private hourRegistrationService: HourService,
    private router: Router
  ) {
    var today = this.getCorrectTodaysDate()

    this.hourRegistrationForm = this.formBuilder.group({
      date: "",
      startTime: [ `${today}T00:00`, 
      [
        Validators.required
      ]],
      endTime: [`${today}T00:00`, [
        Validators.required
      ]],
      recreation: "0",
      description: "",
      employerId: ""
    });
  }

  private getCorrectTodaysDate(){
    var today = new Date();

    var day = today.getDate().toString().length == 1 ? `0${today.getDate()}` : today.getDate()
    var month = (today.getMonth()).toString().length == 1 ? `0${today.getMonth()+1}` : today.getMonth()+1
    var year = today.getUTCFullYear()

    return `${year}-${month}-${day}`
  }

  onStartDateChange(){
    var startDate = new Date(this.hourRegistrationForm.value.startTime)

    var day = startDate.getDate().toString().length == 1 ? `0${startDate.getDate()}` : startDate.getDate()
    var month = (startDate.getMonth()).toString().length == 1 ? `0${startDate.getMonth()+1}` : startDate.getMonth()+1
    var year = startDate.getUTCFullYear()

    var endtime = this.hourRegistrationForm.value.endTime.toString().match("(?=T).*")

    this.hourRegistrationForm.patchValue({endTime: `${year}-${month}-${day}${endtime}`})
  }

  registerHours(){
    console.log(new HourRegistration(this.hourRegistrationForm.value))
    this.hourRegistrationService.addHour(new HourRegistration(this.hourRegistrationForm.value)).subscribe(resp => {
      this.router.navigate(['/hour-overview']);
    })
  }
}

