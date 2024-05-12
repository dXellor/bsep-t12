import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthPageComponent } from './pages/auth-page/auth-page.component';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { authGuard } from './guards/auth.guard';
import { unauthGuard } from './guards/unauth.guard';

const routes: Routes = [
  { path: 'signup', component: AuthPageComponent, canActivate: [unauthGuard] },
  { path: '', component: HomePageComponent, canActivate: [authGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
