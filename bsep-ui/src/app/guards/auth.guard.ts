import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { Observable, map } from 'rxjs';

export const authGuard: CanActivateFn = (route, state): Observable<boolean> => {
  const authService = inject(AuthService);
  const router = inject(Router);

  return authService.loggedInUser$.pipe(
    map((loggedInUser) => {
      if (loggedInUser === undefined) {
        authService.checkAccessTokenValidity();
      }

      if (loggedInUser == null) {
        router.navigateByUrl('/signup');
        return false;
      } else {
        return true;
      }
    })
  );
};
