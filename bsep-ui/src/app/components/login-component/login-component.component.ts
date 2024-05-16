import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { getFormValidationErrors } from 'src/app/helpers/form-helpers';
import { LoginRequest } from 'src/app/models/requests/login-request-interface';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login-component',
  templateUrl: './login-component.component.html',
  styleUrls: ['./login-component.component.scss'],
})
export class LoginComponentComponent implements OnInit {
  @Output() navigateToRegister = new EventEmitter<void>();
  public form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private toastr: ToastrService
  ) {
    this.form = fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.authService.loggedInUser$.subscribe((res) => {
      if (res) {
        this.router.navigateByUrl('');
      }
    });
  }

  public login(): void {
    if (this.form.valid) {
      const loginRequest: LoginRequest = {
        email: this.form.get('email')?.value,
        password: this.form.get('password')?.value,
      };

      this.authService.login(loginRequest);
      const errors = getFormValidationErrors(this.form);
      const message = `${errors[0].control} is ${errors[0].error}`;

      this.toastr.error(message, 'Login error', {
        closeButton: true,
        progressBar: true,
        extendedTimeOut: 2000,
      });
    }
  }

  public showRegister() {
    this.navigateToRegister.emit();
  }
}
