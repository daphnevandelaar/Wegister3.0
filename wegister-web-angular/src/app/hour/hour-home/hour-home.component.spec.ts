import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HourHomeComponent } from './hour-home.component';

describe('HourHomeComponent', () => {
  let component: HourHomeComponent;
  let fixture: ComponentFixture<HourHomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HourHomeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HourHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
