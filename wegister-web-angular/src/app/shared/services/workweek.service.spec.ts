import { TestBed } from '@angular/core/testing';

import { WorkweekService } from './workweek.service';

describe('WorkweekService', () => {
  let service: WorkweekService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WorkweekService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
