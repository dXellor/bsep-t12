import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-two-factor-login-form',
  templateUrl: './two-factor-login-form.component.html',
  styleUrls: ['./two-factor-login-form.component.scss'],
})
export class TwoFactorLoginFormComponent {
  public form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private toastr: ToastrService
  ) {
    this.form = fb.group({
      code: ['', [Validators.required]],
    });
  }

  public login() {
    if (this.form.valid) {
      this.authService.login2Fa(this.form.get('code')?.value);
    } else {
      this.toastr.error('Insert valid code', 'Validation error', {
        closeButton: true,
        progressBar: true,
        extendedTimeOut: 2000,
      });
    }
  }
}
