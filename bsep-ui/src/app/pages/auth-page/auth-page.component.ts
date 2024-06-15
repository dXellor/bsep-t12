import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-auth-page',
  templateUrl: './auth-page.component.html',
  styleUrls: ['./auth-page.component.scss'],
})
export class AuthPageComponent {
  public formType: AuthFormEnum = AuthFormEnum.LOGIN;
  
  constructor(private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
     this.activatedRoute.queryParams.subscribe(params => {
        const action = params['action'];
        if(action == "rp") {
          this.changeForm("PASSWORD_RESET");
        }
      });
  }

  public changeForm(type: string): void {
    this.formType = type as AuthFormEnum;
  }
}

export enum AuthFormEnum {
  LOGIN = 'LOGIN',
  REGISTER = 'REGISTER',
  PASSWORD_RESET_REQUEST = 'PASSWORD_RESET_REQUEST',
  PASSWORD_RESET = 'PASSWORD_RESET',
}
