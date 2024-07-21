import { TestBed } from '@angular/core/testing';

import { ErrorHandelingService } from './error-handeling.service';

describe('ErrorHandelingService', () => {
  let service: ErrorHandelingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ErrorHandelingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
