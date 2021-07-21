import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployerDropdownComponent } from './employer-dropdown.component';

describe('EmployerDropdownComponent', () => {
  let component: EmployerDropdownComponent;
  let fixture: ComponentFixture<EmployerDropdownComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployerDropdownComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployerDropdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
