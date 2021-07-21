import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployerRegisterComponent } from './employer-register.component';

describe('EmployerRegisterComponent', () => {
  let component: EmployerRegisterComponent;
  let fixture: ComponentFixture<EmployerRegisterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployerRegisterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployerRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
