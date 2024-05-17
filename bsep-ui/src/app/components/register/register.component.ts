import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { getFormValidationErrors } from 'src/app/helpers/form-helpers';
import { PackageTypeEnum } from 'src/app/models/enums/package-type-enum';
import { UserTypeEnum } from 'src/app/models/enums/user-type-enum';
import { RegistrationRequest } from 'src/app/models/requests/registration-request-interface';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  @Output() navigateToLogin = new EventEmitter<void>();
  public selectedEntityType: string = 'PhysicalEntity';
  public selectedUserPackage: string = 'Basic';

  constructor(
    private authService: AuthService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  registrationForm = new FormGroup({
    email: new FormControl('', [
      Validators.required,
      Validators.email,
      Validators.max(255),
    ]),
    password: new FormControl('', [Validators.required]),
    password2: new FormControl('', [Validators.required]),
    firstName: new FormControl('', [Validators.required, Validators.max(25)]), //Dynamic validators
    lastName: new FormControl('', [Validators.required, Validators.max(25)]), //Dynamic validators
    companyName: new FormControl('', []), //Dynamic validators
    companyPib: new FormControl('', []), //Dynamic validators
    address: new FormControl('', [Validators.required, Validators.max(255)]),
    city: new FormControl('', [Validators.required, Validators.max(255)]),
    country: new FormControl('', [Validators.required, Validators.max(255)]),
    phone: new FormControl('', [
      Validators.required,
      Validators.pattern('[+]?[ 0-9]{10,20}'),
    ]),
  });

  register(): void {
    let errorMessage: string | null = null;

    if (this.selectedEntityType === '') {
      errorMessage = 'Select entity type';
    }

    if (this.selectedUserPackage === '') {
      errorMessage = 'Select package';
    }

    const password1 = this.registrationForm.get('password')?.value;
    const password2 = this.registrationForm.get('password2')?.value;
    if (password1 !== password2) {
      errorMessage = 'Passwords do not match';
    }

    if (this.registrationForm.valid && errorMessage === null) {
      const registerRequest: RegistrationRequest = {
        email: this.registrationForm.get('email')?.value || '',
        password: this.registrationForm.get('password')?.value || '',
        passwordAgain: this.registrationForm.get('password2')?.value || '',
        firstName: this.registrationForm.get('firstName')?.value || undefined,
        lastName: this.registrationForm.get('lastName')?.value || undefined,
        companyName:
          this.registrationForm.get('companyName')?.value || undefined,
        companyPib: this.registrationForm.get('companyPib')?.value || undefined,
        address: this.registrationForm.get('address')?.value || '',
        country: this.registrationForm.get('country')?.value || '',
        city: this.registrationForm.get('city')?.value || '',
        phone: this.registrationForm.get('phone')?.value || '',
        package: this.selectedUserPackage as PackageTypeEnum,
        type: this.selectedEntityType as UserTypeEnum,
      };

      this.authService.register(registerRequest).subscribe({
        next: () => {
          this.navigateToLogin.emit();
          this.toastr.success(
            'Registration successful, now wait for the admin approval',
            'Registration success',
            {
              closeButton: true,
              progressBar: true,
              extendedTimeOut: 2000,
            }
          );
        },
        error: (error) => {
          this.toastr.error(error.message, 'Registration error', {
            closeButton: true,
            progressBar: true,
            extendedTimeOut: 2000,
          });
        },
      });
    } else {
      const errors = getFormValidationErrors(this.registrationForm);
      if (errors) {
        errorMessage = `${errors[0].control} is ${errors[0].error}`;
      }

      this.toastr.error(errorMessage!, 'Registration error', {
        closeButton: true,
        progressBar: true,
        extendedTimeOut: 2000,
      });
    }
  }

  showLogin(): void {
    this.navigateToLogin.emit();
  }

  changeValidationsBasedOnEntityType(v: string): void {
    if (v === 'LegalEntity') {
      this.registrationForm.get('firstName')?.setValidators([]);
      this.registrationForm.get('lastName')?.setValidators([]);
      this.registrationForm
        .get('companyName')
        ?.setValidators([Validators.required, Validators.max(50)]);
      this.registrationForm
        .get('companyPib')
        ?.setValidators([Validators.required]);
    } else {
      this.registrationForm.get('companyName')?.setValidators([]);
      this.registrationForm.get('companyPib')?.setValidators([]);
      this.registrationForm
        .get('firstName')
        ?.setValidators([Validators.required, Validators.max(25)]);
      this.registrationForm
        .get('lastName')
        ?.setValidators([Validators.required, Validators.max(25)]);
    }

    this.registrationForm.get('firstName')?.updateValueAndValidity();
    this.registrationForm.get('lastName')?.updateValueAndValidity();
    this.registrationForm.get('companyName')?.updateValueAndValidity();
    this.registrationForm.get('companyPib')?.updateValueAndValidity();
  }
}
