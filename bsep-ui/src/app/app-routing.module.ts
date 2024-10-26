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
import { AdministratorUserManagingPageComponent } from './pages/administrator-user-managing-page/administrator-user-managing-page.component';
import { AdsPageComponent } from './pages/ads-page/ads-page/ads-page.component';
import { administratorGuard } from './guards/administrator.guard';

const routes: Routes = [
  { path: 'signup', component: AuthPageComponent, canActivate: [unauthGuard] },
  { path: '', component: HomePageComponent, canActivate: [authGuard] },
  {
    path: 'enable-2fa',
    component: EnableTfaPageComponent,
    canActivate: [authGuard, administratorGuard],
  },
  {
    path: 'admin-profile',
    component: AdministratorProfilePageComponent,
    canActivate: [authGuard, administratorGuard],
  },
  {
    path: 'admin-user-managing',
    component: AdministratorUserManagingPageComponent,
    canActivate: [authGuard, administratorGuard],
  },
  {
    path: 'two-factor',
    component: TfaPageComponent,
    canActivate: [twoFaGuard],
  },
  {
    path: 'ads',
    component: AdsPageComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
