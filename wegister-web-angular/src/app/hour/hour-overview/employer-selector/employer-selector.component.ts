import { Component, EventEmitter, Output, Input } from '@angular/core';
import { Employer } from 'src/app/models/employer.model';
import { EmployerService } from 'src/app/employer/employer.service';

@Component({
  selector: 'app-employer-selector',
  templateUrl: './employer-selector.component.html',
  styleUrls: ['./employer-selector.component.css']
})
export class EmployerSelectorComponent {
  
  buttonEmployerText = "Werkgever"
  employers: Employer[] = [];

  @Output() selectedEmployer = new EventEmitter<string>();
  @Input() employeeNumber: number = 0;

  constructor(
    private employerService: EmployerService
  ) { 
    let allemp: Employer = {id: "0", name: "All", email: ""}
    this.employers.push(allemp);

    this.employerService.getEmployers().subscribe(emps => {
      emps.forEach(element =>{
        this.employers.push(element);
      });

      if(this.employeeNumber != 0){
        this.buttonEmployerText = this.employers[this.employeeNumber].name
      }
      if(this.employeeNumber != 0){
        this.employers.splice(0, 1);
      }
    });
  }

  selectEmployer(employer: Employer){
    if(employer.id == "0"){
      this.buttonEmployerText = `All`;
    }
    else{
      this.buttonEmployerText = employer.name;
    }
    this.selectedEmployer.emit(employer.id.toString());
  }
}
