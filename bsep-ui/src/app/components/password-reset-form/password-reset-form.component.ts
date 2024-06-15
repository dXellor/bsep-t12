import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { PasswordResetRequest } from 'src/app/models/requests/password-reset-request-interface';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-password-reset-form',
  templateUrl: './password-reset-form.component.html',
  styleUrls: ['./password-reset-form.component.scss']
})
export class PasswordResetFormComponent {
  @Output() navigateToLogin = new EventEmitter<void>();
  public form: FormGroup;
  private passwordResetRequest: PasswordResetRequest = {email: "", token: "", password: ""};

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private toastr: ToastrService,
    private activatedRoute: ActivatedRoute
  ) {
    this.form = fb.group({
      password: ['', [Validators.required]],
      password2: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.authService.loggedInUser$.subscribe((res) => {
      if (res) {
        this.router.navigateByUrl('');
      }
    });

    this.activatedRoute.queryParams.subscribe(params => {
      const action = params['action'];
      this.passwordResetRequest = {
        email: params['user'],
        token: params['token'],
        password: "",
      } 
    });
  }

  public resetPassword(): void {
    this.passwordResetRequest.password = this.form.get('password')?.value;
    try {
      this.authService.resetPassword(this.passwordResetRequest).subscribe((res) => {
        this.toastr.success('Password reset success', 'Success', {
          closeButton: true,
          progressBar: true,
          extendedTimeOut: 2000,
        });
        this.navigateToLogin.emit();
      });
    } catch(err) {
      this.toastr.error('Your link seems to be invalid', 'Error', {
        closeButton: true,
        progressBar: true,
        extendedTimeOut: 2000,
      });
      this.navigateToLogin.emit();
    }
  }
}
