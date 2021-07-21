import { Component, OnInit } from '@angular/core';
import { WorkWeek } from '../models/workweek-model';
import { WorkweekService } from '../shared/services/workweek.service';
import { EmailService } from '../shared/services/email.service';


@Component({
  selector: 'app-mail',
  templateUrl: './mail.component.html',
  styleUrls: ['./mail.component.css']
})
export class MailComponent implements OnInit {

  workweeks: WorkWeek[] = [];

  constructor(
    private hourRegisterService: WorkweekService,
    private mailService: EmailService
  ) { }

  ngOnInit(): void {
    this.hourRegisterService.getWeeksPerYear(2020).subscribe(workweeks => {
        this.workweeks = workweeks;
    });
  }

  MailHours(){
    this.mailService.mailByWeekIds(this.arrayWeekIds).subscribe( response => {
      window.location.reload();
    });
  }

  arrayWeekIds: any[] = [];

  onChecked(week, event){
    if(!event)
      this.arrayWeekIds.splice(this.arrayWeekIds.indexOf(week.id), 1)
    if(event)  
      this.arrayWeekIds.push(week.id)
  }
}
