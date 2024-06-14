import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, filter, map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../models/user-interface';
import { RegistrationRequest } from '../models/requests/registration-request-interface';
import { LoginRequest } from '../models/requests/login-request-interface';
import { LoginResponse } from '../models/responses/login-response-interface';
import { ToastrService } from 'ngx-toastr';
import { UserRoleEnum } from '../models/enums/user-role-enum';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private url: string = `${environment.apiUrl}/Auth`;
  public loggedInUser$ = new BehaviorSubject<User | null | undefined>(
    undefined
  );

  constructor(
    private http: HttpClient,
    private toastr: ToastrService,
    private router: Router
  ) {}

  public register(request: RegistrationRequest): Observable<User> {
    return this.http.post<User>(`${this.url}/register`, request);
  }

  public login(request: LoginRequest): void {
    this.http.post<LoginResponse>(`${this.url}/login`, request).subscribe({
      next: (res) => {
        if (!res.redirectToTotp) {
          this.saveLoggedInUser(res);
        } else {
          window.localStorage.setItem('email-for-2fa', res.user.email);
          this.router.navigateByUrl('/two-factor');
        }
      },
      error: (error) => {
        this.clearLoggedInUser(),
          this.toastr.error('Invalid credentials', 'Login error', {
            closeButton: true,
            progressBar: true,
            extendedTimeOut: 2000,
          });
      },
    });
  }

  public refreshAccessToken(): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.url}/refresh`, null);
  }

  public checkAccessTokenValidity() {
    this.http.get<User>(`${this.url}/accessCheck`).subscribe({
      next: (res) => this.loggedInUser$.next(res),
      error: (error) => this.clearLoggedInUser(),
    });
  }

  public saveLoggedInUser(loginResponse: LoginResponse): void {
    window.localStorage.setItem('jwt', loginResponse.accessToken);
    this.loggedInUser$.next(loginResponse.user);
  }

  public clearLoggedInUser(): void {
    window.localStorage.removeItem('jwt');
    this.loggedInUser$.next(null);
  }

  public hasRole(role: UserRoleEnum): Observable<boolean> {
    return this.loggedInUser$.pipe(
      filter((user) => !!user),

      // Map to a boolean indicating whether the user has the specified role
      map((user) => user?.role === role)
    );
  }

  public requestReCaptchaScore(token: string) {
    return this.http.get(`${this.url}/recaptchaAssessment/${token}`);
  }

  public getTwoFAQr() {
    return this.http.get<string>(`${this.url}/totpQr`);
  }

  public enable2Fa(code: string) {
    const body = {
      email: '',
      totp: code,
    };

    return this.http.post<boolean>(`${this.url}/enable2Fa`, body);
  }

  public login2Fa(code: string): void {
    const body = {
      email: window.localStorage.getItem('email-for-2fa'),
      totp: code,
    };

    this.http.post<LoginResponse>(`${this.url}/totp`, body).subscribe({
      next: (res) => {
        window.localStorage.removeItem('email-for-2fa');
        this.saveLoggedInUser(res);
      },
      error: (error) => {
        this.toastr.error('Invalid code', '2FA Login error', {
          closeButton: true,
          progressBar: true,
          extendedTimeOut: 2000,
        });
      },
    });
  }

  
}
