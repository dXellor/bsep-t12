import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { Observable, map } from 'rxjs';
import {UserRoleEnum} from "../models/enums/user-role-enum";

export const clientGuard: CanActivateFn = (route, state): Observable<boolean> => {
  const authService = inject(AuthService);
  const router = inject(Router);

  return authService.loggedInUser$.pipe(
    map((loggedInUser) => {
      if (loggedInUser === undefined) {
        authService.checkAccessTokenValidity();
      }

      if (authService.hasRole(UserRoleEnum.Client)) {
        return true;
      } else {
        router.navigateByUrl('/signup');
        return false;
      }
    })
  );
};
