import { Component, OnInit } from '@angular/core';
import { Advertisement } from "../../../models/advertisement";
import { User } from "../../../models/user-interface";
import { AdsService } from "../../../services/ads.service";
import { ClickRequest } from "../../../models/click-request";
import {UserService} from "../../../services/user.service";
import {AuthService} from "../../../services/auth.service";

@Component({
  selector: 'app-ads-page',
  templateUrl: './ads-page.component.html',
  styleUrls: ['./ads-page.component.scss']
})
export class AdsPageComponent implements OnInit {
  ads: Advertisement[] = []
  public loggedInUser?: User;

  constructor(private service: AdsService, private authService: AuthService) { }

  ngOnInit() {
    this.service.getAllAds().subscribe((res) => {
      this.ads = res;
      console.log(this.ads);
    });
    this.authService.loggedInUser$.subscribe((res) => {
      if (res) {
        this.loggedInUser = res;
        console.log(this.loggedInUser + " " + res)
      }
    });
  }

  clickAd() {
    let request: ClickRequest = {
      email: this.loggedInUser?.email,
      package: this.loggedInUser?.package
    }
    console.log(request)
    this.service.click(request).subscribe(response => {
      if (response.status === 200) {
        alert('Click registered successfully');
      }
    }, error => {
      if (error.status === 429) {
        alert('Click limit reached. Please try again later.');
      }
    });
  }
}
