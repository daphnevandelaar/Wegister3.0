import { Component, OnInit } from '@angular/core';
import { HourRegisterService } from '../hour-register/hour-register.service';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnInit {

  constructor(
    private hourRegistrationService: HourRegisterService
  ) {  }

  ngOnInit(): void {
  }

  OnClick(newApi){
    console.log(newApi);
    this.hourRegistrationService.testRegistration(newApi).subscribe(c => console.log(c));
  }
}
