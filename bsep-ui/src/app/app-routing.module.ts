import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthPageComponent } from './pages/auth-page/auth-page.component';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { authGuard } from './guards/auth.guard';
import { unauthGuard } from './guards/unauth.guard';
import { AdministratorProfilePageComponent } from './pages/administrator-profile-page/administrator-profile-page.component';
import { UserRoleEnum } from './models/enums/user-role-enum';
import { administratorAuthGard } from './guards/administrator.guard';

const routes: Routes = [
  { path: 'signup', component: AuthPageComponent, canActivate: [unauthGuard] },
  { path: '', component: HomePageComponent, canActivate: [authGuard] },
  {
    path: 'admin-profile',
    component: AdministratorProfilePageComponent,
    canActivate: [authGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
