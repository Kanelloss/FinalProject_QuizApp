import { 
  HttpHandler,
  HttpInterceptor,
  HttpRequest
} from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class AuthInterceptorService implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler) {
      const authToken = localStorage.getItem('access_token'); // Τράβα το token από το localStorage
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
