import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
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

  constructor(private authService: AuthService, private router: Router) {}

  registrationForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [
      Validators.required,
      Validators.pattern('[+]?[ 0-9]{10,20}'),
    ]),
    password2: new FormControl('', [Validators.required]),
    firstName: new FormControl('', [Validators.required]),
    lastName: new FormControl('', [Validators.required]),
    companyName: new FormControl('', [Validators.required]),
    pib: new FormControl('', [Validators.required]),
    address: new FormControl('', [Validators.required]),
    city: new FormControl('', [Validators.required]),
    country: new FormControl('', [Validators.required]),
    phone: new FormControl('', [Validators.required]),
    package: new FormControl('', [Validators.required]),
  });

  register(): void {}

  showLogin(): void {
    this.navigateToLogin.emit();
  }
}
