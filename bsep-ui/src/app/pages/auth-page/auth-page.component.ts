import { Component } from '@angular/core';

@Component({
  selector: 'app-auth-page',
  templateUrl: './auth-page.component.html',
  styleUrls: ['./auth-page.component.scss'],
})
export class AuthPageComponent {
  public formType: AuthFormEnum = AuthFormEnum.LOGIN;

  public changeForm(type: string): void {
    this.formType = type as AuthFormEnum;
  }
}

export enum AuthFormEnum {
  LOGIN = 'LOGIN',
  REGISTER = 'REGISTER',
}
