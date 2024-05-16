import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../models/user-interface';
import { RegistrationRequest } from '../models/requests/registration-request-interface';
import { LoginRequest } from '../models/requests/login-request-interface';
import { LoginResponse } from '../models/responses/login-response-interface';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private url: string = `${environment.apiUrl}/Auth`;
  public loggedInUser$ = new BehaviorSubject<User | null | undefined>(
    undefined
  );

  constructor(private http: HttpClient, private toastr: ToastrService) {}

  public register(request: RegistrationRequest): Observable<User> {
    return this.http.post<User>(`${this.url}/register`, request);
  }

  public login(request: LoginRequest): void {
    this.http.post<LoginResponse>(`${this.url}/login`, request).subscribe({
      next: (res) => this.saveLoggedInUser(res),
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
}
