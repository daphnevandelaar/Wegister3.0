import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HourOverviewComponent } from './hour-overview.component';

describe('HourOverviewComponent', () => {
  let component: HourOverviewComponent;
  let fixture: ComponentFixture<HourOverviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HourOverviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HourOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
