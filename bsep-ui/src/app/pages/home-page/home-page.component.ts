import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user-interface';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss'],
})
export class HomePageComponent implements OnInit {
  public loggedInUser?: User;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.loggedInUser$.subscribe((res) => {
      if (res) {
        this.loggedInUser = res;
      }
    });
  }
}