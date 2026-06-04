import { CanActivateFn } from '@angular/router';
import { Router } from '@angular/router';
import { inject } from '@angular/core';

export const loginGuard: CanActivateFn = () => {

  const router = inject(Router);

  if (typeof window === 'undefined') {
    return true;
  }

  const token = localStorage.getItem('token');

  if (token) {

    router.navigate([
      '/purchase-orders'
    ]);

    return false;
  }

  return true;
};