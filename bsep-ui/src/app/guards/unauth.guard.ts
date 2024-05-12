import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { Observable, map } from 'rxjs';
import { AuthService } from '../services/auth.service';

export const unauthGuard: CanActivateFn = (
  route,
  state
): Observable<boolean> => {
  const authService = inject(AuthService);
  const router = inject(Router);

  return authService.loggedInUser$.pipe(
    map((loggedInUser) => {
      if (loggedInUser === undefined) {
        authService.checkAccessTokenValidity();
      }

      if (loggedInUser != null) {
        router.navigateByUrl('');
        return false;
      } else {
        return true;
      }
    })
  );
};
