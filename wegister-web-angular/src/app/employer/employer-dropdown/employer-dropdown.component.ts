import { Component, Input } from '@angular/core';
import { EmployerService } from '../employer.service';
import { Employer } from 'src/app/models/employer.model';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-employer-dropdown',
  templateUrl: './employer-dropdown.component.html',
  styleUrls: ['./employer-dropdown.component.css']
})
export class EmployerDropdownComponent {
  buttonEmployerText = "Werkgever"
  employers: Employer[] = [];
  private selectedEmployerId = "0";
  @Input() hourRegistrationForm: FormGroup;

  constructor(
    private employerService: EmployerService
  ) { 
    this.employerService.getEmployers().subscribe(emps => this.employers = emps);
  }

  selectEmployer(employer: Employer){
    this.selectedEmployerId = employer.id;
    this.hourRegistrationForm.patchValue({employerId: this.selectedEmployerId.toString()})
    this.buttonEmployerText = employer.name;
  }
}
