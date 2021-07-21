import { TestBed } from '@angular/core/testing';

import { HourRegisterService } from './hour-register.service';

describe('HourRegisterService', () => {
  let service: HourRegisterService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HourRegisterService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
