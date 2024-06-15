import { Component } from '@angular/core';
import { User } from 'src/app/models/user-interface';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-administrator-user-managing-page',
  templateUrl: './administrator-user-managing-page.component.html',
  styleUrls: ['./administrator-user-managing-page.component.scss']
})
export class AdministratorUserManagingPageComponent {
  public allUsers: User[] = [];

  constructor(private userService: UserService){}

  ngOnInit(): void {
    this.getUsers();
  }
  
  
  getUsers(): void {
    this.userService.getAllUsers().subscribe(result => {
      this.allUsers = result;
    });
  }
}
