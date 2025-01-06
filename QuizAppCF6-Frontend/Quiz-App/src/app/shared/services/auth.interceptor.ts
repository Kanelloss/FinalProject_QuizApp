import { 
  HttpHandler,
  HttpInterceptor,
  HttpRequest
} from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { UserService } from './user.service';
import { MatDialog } from '@angular/material/dialog';
import { AlertDialogComponent } from '../components/alert-dialog/alert-dialog.component';

@Injectable()
export class AuthInterceptorService implements HttpInterceptor {
  userService = inject(UserService);
  dialog = inject(MatDialog);

  intercept(req: HttpRequest<any>, next: HttpHandler) {
      const authToken = localStorage.getItem('access_token'); // Pull token from local storage

      // Check if the token is expired
    if (this.userService.isTokenExpired()) {
      console.warn('[AuthInterceptor] Token expired. Logging out...');
      this.userService.logoutUser(); // Perform logout if token is expired.

      // Stop further processing of the request
      return next.handle(req.clone({})); // Return an empty observable
    }

      // if (this.userService.isTokenExpired()) {
      //   console.warn('[AuthInterceptor] Token expired. Logging out...');
      //   this.userService.logoutUser(); // Automatically log out if the token is expired.
      //   return next.handle(req); // Proceed with the request, but the user will be logged out.
      // }
      
      if (!authToken) {
          console.log('[AuthInterceptor] No token found.');
          return next.handle(req);
      }

      console.log('[AuthInterceptor] Token added to request headers.');
      const authRequest = req.clone({
          headers: req.headers.set('Authorization', 'Bearer ' + authToken)
      });

      return next.handle(authRequest);
  }
}
