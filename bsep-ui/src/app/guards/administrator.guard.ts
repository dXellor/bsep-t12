import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { UserRoleEnum } from '../models/enums/user-role-enum';

@Injectable({
  providedIn: 'root'
})
export class administratorAuthGard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) {}

  canActivate(): Observable<boolean> {
    return this.authService.loggedInUser$.pipe(
      map(loggedInUser => {
        if (!loggedInUser) {
          this.authService.checkAccessTokenValidity();
          return false;
        }
        if (loggedInUser.role === UserRoleEnum.Administrator) {
          return true;
        } else {
          this.router.navigateByUrl('/signup');
          return false;
        }
      })
    );
  }
}
