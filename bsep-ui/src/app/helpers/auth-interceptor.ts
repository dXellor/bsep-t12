import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, from, switchMap, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { LoginResponse } from '../models/responses/login-response-interface';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService, private router: Router) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (req.url.includes('refresh')) return next.handle(req);

    const token = localStorage.getItem('jwt');
    if (token) {
      const cloned = req.clone({
        headers: req.headers.set('Authorization', 'Bearer ' + token),
      });

      return next.handle(cloned).pipe(
        catchError((error: HttpErrorResponse) => {
          if (error?.status == 401) {
            return this.refreshTokenMethod(cloned, next);
          } else {
            return throwError(() => error);
          }
        })
      );
    } else {
      return next.handle(req);
    }
  }

  refreshTokenMethod(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return from(this.authService.refreshAccessToken()).pipe(
      switchMap((res: LoginResponse) => {
        this.authService.saveLoggedInUser(res);
        request = request.clone({
          setHeaders: {
            Authorization: 'Bearer ' + res.accessToken,
          },
        });

        return next.handle(request);
      }),

      catchError((error) => {
        if (error.status == 401) {
          this.redirectLogout();
        }

        return throwError(() => error);
      })
    );
  }

  redirectLogout() {
    this.authService.clearLoggedInUser();
    this.router.navigateByUrl('/signup');
  }
}
