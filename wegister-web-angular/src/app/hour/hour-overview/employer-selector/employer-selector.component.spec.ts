import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployerSelectorComponent } from './employer-selector.component';

describe('EmployerSelectorComponent', () => {
  let component: EmployerSelectorComponent;
  let fixture: ComponentFixture<EmployerSelectorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployerSelectorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployerSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
