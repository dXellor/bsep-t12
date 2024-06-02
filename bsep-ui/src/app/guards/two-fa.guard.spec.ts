import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { twoFaGuard } from './two-fa.guard';

describe('twoFaGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => twoFaGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
