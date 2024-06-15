import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-password-reset-request-form',
  templateUrl: './password-reset-request-form.component.html',
  styleUrls: ['./password-reset-request-form.component.scss']
})
export class PasswordResetRequestFormComponent {
  @Output() navigateToLogin = new EventEmitter<void>();
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
  
  public showLogin() {
    this.navigateToLogin.emit();
  }

  public sendPasswordResetRequest(): void {
    const email = this.form.get('email')?.value;
    this.authService.startPasswordReset(email).subscribe((res) => {
      console.log(res); // improve with toast message
    });
  }
}
