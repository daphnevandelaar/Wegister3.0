import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { EmployerService } from '../employer.service';
import { Employer } from 'src/app/models/employer.model';

@Component({
  selector: 'app-employer-register',
  templateUrl: './employer-register.component.html',
  styleUrls: ['./employer-register.component.css']
})
export class EmployerRegisterComponent implements OnInit {

  employerRegistrationForm;
  employers: Employer[] = [];
  employerToChange: Employer; 

  constructor(
    private formBuilder: FormBuilder,
    private employerService: EmployerService
  ) { 
    this.employerRegistrationForm = this.formBuilder.group({
      employerName: '',
      employerEmail: ''
    });
    this.employerService.getEmployers().subscribe(emps => this.employers = emps);
  }

  ngOnInit(): void {
  }

  registerEmployer(){
    let employer = <Employer>{
      name: this.employerRegistrationForm.value.employerName, 
      email: this.employerRegistrationForm.value.employerEmail 
    }    
    this.employerService.addEmployer(employer).subscribe(t => window.location.reload());
    employer.id = (this.employers.length +1).toString();
    this.employers.push(employer);
  }
  onClickChange(employer){
    this.employerToChange = employer;
  }

  changeEmployer(employer){
    this.employerService.updateEmployer(employer).subscribe( response => {
      window.location.reload();
    });
  }

  onChangeEmail($event){
    this.employerToChange.email = $event;
  }

  onChangeName($event){
    this.employerToChange.name = $event;
  }
  onClickDelete(){
    this.employerService.deleteEmployer(this.employerToChange).subscribe( resp => window.location.reload());
  }
}
