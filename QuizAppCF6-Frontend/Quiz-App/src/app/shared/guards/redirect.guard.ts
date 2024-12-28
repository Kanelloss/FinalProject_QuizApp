import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../services/user.service';

export const redirectGuard: CanActivateFn = (route, state) => {
  const userService = inject(UserService);
  const router = inject(Router);

  if (userService.isLoggedIn()) {
    return router.navigate(['home']); // Αν ο χρήστης είναι συνδεδεμένος, ανακατεύθυνση στο home
  }
  return true; // Επιτρέπουμε την πρόσβαση αν δεν είναι συνδεδεμένος
};
