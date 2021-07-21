import { TestBed } from '@angular/core/testing';

import { HourService } from './hour.service';

describe('HourService', () => {
  let service: HourService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HourService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
