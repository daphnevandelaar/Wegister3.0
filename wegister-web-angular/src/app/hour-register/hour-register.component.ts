import { Component } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HourRegisterService } from './hour-register.service';
import { ComparisonDateValidator } from './comparison-date.directive';
import { HourRegistration } from '../models/hour-registration-model';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-hour-register',
  templateUrl: './hour-register.component.html',
  styleUrls: ['./hour-register.component.scss']
})
export class HourRegisterComponent {

  hourRegistrationForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private hourRegistrationService: HourRegisterService,
    private router: Router,
    private authservice: OAuthService
  ) {
    var today = this.getCorrectTodaysDate()

    this.hourRegistrationForm = this.formBuilder.group({
      date: '',
      startTime: [ `${today}T00:00`, 
      [
        Validators.required
      ]],
      endTime: [`${today}T00:00`, [
        Validators.required
      ]],
      recreation: "0",
      description: '',
      employerId: ""
    }, { validator : ComparisonDateValidator});

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
    this.hourRegistrationService.postHour(new HourRegistration(this.hourRegistrationForm.value)).subscribe(resp => {
      this.router.navigate(['/hour-overview']);
    })
  }
}
