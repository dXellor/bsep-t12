import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { AuthService } from '../../services/auth.service';
import { User } from '../../models/user-interface';
import { RoleChangeRequest } from '../../models/requests/role-change-request';
import { UserRoleEnum } from '../../models/enums/user-role-enum';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {Subscription} from "rxjs";

@Component({
  selector: 'app-administrator-profile-page',
  templateUrl: './administrator-profile-page.component.html',
  styleUrls: ['./administrator-profile-page.component.scss']
})
export class AdministratorProfilePageComponent implements OnInit {
  users: User[] = [];
  updateUserForm: FormGroup;
  loggedInUser: User | null | undefined;
  userSubscription: Subscription = {} as Subscription;

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private formBuilder: FormBuilder
  ) {
    this.updateUserForm = this.formBuilder.group({
      firstName: ['', [Validators.required, Validators.maxLength(25)]],
      lastName: ['', [Validators.required, Validators.maxLength(25)]],
      companyName: ['', [Validators.required, Validators.maxLength(50)]],
      companyPib: ['', [Validators.required, Validators.maxLength(9)]],
      address: ['', [Validators.required, Validators.maxLength(255)]],
      city: ['', [Validators.required, Validators.maxLength(255)]],
      country: ['', [Validators.required, Validators.maxLength(255)]],
      phone: ['', [Validators.required, Validators.pattern('[+]?[ 0-9]{10,20}')]],
      package: ['Basic', Validators.required]
    });
  }

  ngOnInit() {
    this.userService.getAllUsers().subscribe(result => {
      this.users = result;
      console.log(result);
    });

    this.userSubscription = this.authService.loggedInUser$.subscribe(user => {
      this.loggedInUser = user;
      console.log(this.loggedInUser);
      if (this.loggedInUser) {
        this.updateFormWithLoggedInUserData();
      }
    });

    if (!this.loggedInUser) {
      this.authService.checkAccessTokenValidity();
    }
  }

  ngOnDestroy() {
    this.userSubscription.unsubscribe();
  }

  updateFormWithLoggedInUserData() {
    if (this.loggedInUser) {
      this.updateUserForm = this.formBuilder.group({
        firstName: [this.loggedInUser.firstName || '', [Validators.required, Validators.maxLength(25)]],
        lastName: [this.loggedInUser.lastName || '', [Validators.required, Validators.maxLength(25)]],
        companyName: [this.loggedInUser.companyName || '', [Validators.required, Validators.maxLength(50)]],
        companyPib: [this.loggedInUser.companyPib || '', [Validators.required, Validators.maxLength(9)]],
        address: [this.loggedInUser.address || '', [Validators.required, Validators.maxLength(255)]],
        city: [this.loggedInUser.city || '', [Validators.required, Validators.maxLength(255)]],
        country: [this.loggedInUser.country || '', [Validators.required, Validators.maxLength(255)]],
        phone: [this.loggedInUser.phone || '', [Validators.required, Validators.pattern('[+]?[ 0-9]{10,20}')]],
        package: ['Basic', Validators.required]
      });
    }
  }

  updateUser() {
    if (this.updateUserForm.valid) {
      const updatedUser: User = {
        ...this.loggedInUser,
        ...this.updateUserForm.value
      };

      this.userService.update(updatedUser).subscribe({
        next: (response) => {
          console.log('User updated successfully:', response);
          this.loggedInUser = response;
          this.authService.saveLoggedInUser({ accessToken: '', user: response });
        },
        error: (error) => {
          console.error('Error updating user:', error);
        }
      });
    } else {
      console.error('Form is invalid');
    }
  }

  changeRole(email: string, newRole: UserRoleEnum) {
    let request = {} as RoleChangeRequest;
    request.email = email;
    request.newRole = newRole;
    console.log(request);
    this.userService.changeRole(request).subscribe(_ => {
      console.log('Success');
    });
  }

  protected readonly UserRoleEnum = UserRoleEnum;
}
