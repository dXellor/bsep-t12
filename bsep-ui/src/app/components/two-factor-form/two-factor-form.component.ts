import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-two-factor-form',
  templateUrl: './two-factor-form.component.html',
  styleUrls: ['./two-factor-form.component.scss'],
})
export class TwoFactorFormComponent implements OnInit {
  public form: FormGroup;
  public qr: any | undefined | null;
  public qrImage: string | undefined = undefined;

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

  ngOnInit(): void {
    this.authService.getTwoFAQr().subscribe({
      next: (res) => {
        this.qr = res;
        this.qrImage = `data:image/jpeg;base64,${this.qr}`;
      },
      error: (error) => {
        this.qr = null;
      },
    });
  }

  public enableTwoFA(): void {
    if (this.form.valid) {
      this.authService.enable2Fa(this.form.get('code')?.value).subscribe({
        next: (res) => {
          this.toastr.success('You have enabled 2FA', 'Command successful', {
            closeButton: true,
            progressBar: true,
            extendedTimeOut: 2000,
          });
          this.router.navigateByUrl('/');
        },

        error: (err) => {
          this.toastr.error('Invalid code', '2FA Error', {
            closeButton: true,
            progressBar: true,
            extendedTimeOut: 2000,
          });
        },
      });
    } else {
      this.toastr.error('Insert valid code', '2FA Error', {
        closeButton: true,
        progressBar: true,
        extendedTimeOut: 2000,
      });
    }
  }
}
