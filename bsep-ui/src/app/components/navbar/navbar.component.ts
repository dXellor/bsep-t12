import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserRoleEnum } from 'src/app/models/enums/user-role-enum';
import { User } from 'src/app/models/user-interface';
import { AuthService } from 'src/app/services/auth.service';
import { MonitoringService } from 'src/app/services/monitoring.service';
import { NotificationService } from 'src/app/services/notification.service';
import { SecretService } from 'src/app/services/secret.service';
import { UserService } from 'src/app/services/user.service';

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
    private userService: UserService,
    private secretService: SecretService,
    private notificationsService: NotificationService,
    private toastr: ToastrService,
    private router: Router,
    private monitoringService: MonitoringService
  ) {}

  ngOnInit(): void {
    this.authService.loggedInUser$.subscribe((res) => {
      if (res) {
        this.loggedInUser = res;
      }
    });
    this.notificationsService.startConnection().subscribe(() => {
      this.notificationsService.receiveMessage().subscribe((message) => {
        this.toastr.error(message, 'ATTENTION ERROR!', {
          closeButton: true,
          progressBar: true,
          extendedTimeOut: 2000,
        });

        if (!('Notification' in window)) {
          console.log('Web Notification not supported');
          return;
        }

        Notification.requestPermission(function (permission) {
          var notification = new Notification('ERROR', {
            body: message,
            icon: 'https://cdn0.iconfinder.com/data/icons/psychology-disorder-aqua-vol-2/500/Panic_Attack-512.png',
            dir: 'auto',
          });
          setTimeout(function () {
            notification.close();
          }, 3000);
        });
      });
    });
  }

  public logOut() {
    this.authService.clearLoggedInUser();
    this.router.navigateByUrl('/signup');
  }

  showAds() {
    this.router.navigateByUrl('/ads');
  }

  public getSecret() {
    this.secretService.getSecret();
  }

  public deleteData() {
    if (
      confirm('Are you sure you want to delete all of your data pernamently?')
    ) {
      this.userService
        .deleteUserByEmail(this.loggedInUser?.email || '')
        .subscribe({
          next: () => {},
          error: (error) => {
            console.error('Error deleting user:', error);
          },
        });
      this.authService.clearLoggedInUser();
      this.router.navigateByUrl('/signup');
    }
  }

  public downloadLogs() {
    this.monitoringService.downloadLatestLog().subscribe((res) => {
      const blob = res.body as Blob;
      var link = document.createElement('a');
      link.href = window.URL.createObjectURL(blob);
      link.download = 'log.txt';
      link.click();
    });
  }
}
