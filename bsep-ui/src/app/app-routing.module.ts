import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthPageComponent } from './pages/auth-page/auth-page.component';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { authGuard } from './guards/auth.guard';
import { unauthGuard } from './guards/unauth.guard';
import { AdministratorProfilePageComponent } from './pages/administrator-profile-page/administrator-profile-page.component';
import { EnableTfaPageComponent } from './pages/enable-tfa-page/enable-tfa-page.component';
import { TfaPageComponent } from './pages/tfa-page/tfa-page.component';
import { twoFaGuard } from './guards/two-fa.guard';
import {AdsPageComponent} from "./pages/ads-page/ads-page/ads-page.component";

const routes: Routes = [
  { path: 'signup', component: AuthPageComponent, canActivate: [unauthGuard] },
  { path: '', component: HomePageComponent, canActivate: [authGuard] },
  {
    path: 'enable-2fa',
    component: EnableTfaPageComponent,
    canActivate: [authGuard],
  },
  {
    path: 'admin-profile',
    component: AdministratorProfilePageComponent,
    canActivate: [authGuard],
  },
  {
    path: 'two-factor',
    component: TfaPageComponent,
    canActivate: [twoFaGuard],
  },
  {
    path: 'ads',
    component: AdsPageComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
