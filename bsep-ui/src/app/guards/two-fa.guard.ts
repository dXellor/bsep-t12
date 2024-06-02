import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const twoFaGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);

  let currentUrl = router.url;
  let isFromLogin = currentUrl.indexOf('/signup') == -1 ? false : true;
  return isFromLogin;
};
