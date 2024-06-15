import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/models/user-interface';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-users-table',
  templateUrl: './users-table.component.html',
  styleUrls: ['./users-table.component.scss']
})
export class UsersTableComponent {
  @Input() public users: User[] = [];

  constructor(
    private userService: UserService,
    private router: Router,
    private toastr: ToastrService
  ) {
  }

  onBlockClicked(user: User): void{
    this.userService.blockUser(user.email).subscribe((res) => {
      this.toastr.success("User blocked", 'Success', {
        closeButton: true,
        progressBar: true,
        extendedTimeOut: 2000,
      });
    });
  }
}
