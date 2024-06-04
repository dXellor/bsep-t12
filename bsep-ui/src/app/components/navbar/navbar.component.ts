import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserRoleEnum } from 'src/app/models/enums/user-role-enum';
import { User } from 'src/app/models/user-interface';
import { AuthService } from 'src/app/services/auth.service';
import { SecretService } from 'src/app/services/secret.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  public loggedInUser?: User;
  protected readonly UserRoleEnum = UserRoleEnum;

  constructor(
    private authService: AuthService,
    private secretService: SecretService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.authService.loggedInUser$.subscribe((res) => {
      if (res) {
        this.loggedInUser = res;
      }
    });
  }

  public logOut() {
    this.authService.clearLoggedInUser();
    this.router.navigateByUrl('/signup');
  }

  public getSecret() {
    this.secretService.getSecret();
  }

  public deleteData() {
    if (confirm('Are you sure you want to delete all of your data pernamently?')) {
      this.authService.deleteUserByEmail(this.loggedInUser?.email || '').subscribe({
        next: () => {
          
        },
        error: (error) => {
          console.error('Error deleting user:', error);
        }
      });
      this.authService.clearLoggedInUser();
      this.router.navigateByUrl('/signup');
    }
  }

  
}
