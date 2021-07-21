import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HourRegisterAutomaticComponent } from './hour-register-automatic.component';

describe('HourRegisterAutomaticComponent', () => {
  let component: HourRegisterAutomaticComponent;
  let fixture: ComponentFixture<HourRegisterAutomaticComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HourRegisterAutomaticComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HourRegisterAutomaticComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
