import { Inject, inject, Injectable, signal, effect } from '@angular/core'; // signal & effect μαζί.
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Credentials, User, LoggedInUser } from '../interfaces/user';
import { environment } from '../../../environments/environment';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';

const apiUrl = `${environment.apiUrl}/User`; // backend URL.
@Injectable({
  providedIn: 'root'
})
export class UserService {
  http: HttpClient = inject(HttpClient);
  router = inject(Router);
  
  user = signal<LoggedInUser | null>(null) // initialize value with null.

  constructor() {
    const accessToken = localStorage.getItem('access_token');
    if (accessToken) {
      this.setUserFromToken(accessToken);
    }

    effect(() => {
      if (this.user()) {
        console.log("User logged in: ", this.user()?.username);
      } else {
        console.log('No user logged in');
      }
    })
  }

   private setUserFromToken(token: string) {
    const decodedToken: any = jwtDecode(token);
    this.user.set({
      id: decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'],
      username: decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
      email: decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
      role: decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
    });
   }

  loginUser(credentials: Credentials) {
    return this.http.post<{token:string}>(`${apiUrl}/login`, credentials)
  }

  /**
   * Εγγράφει έναν νέο χρήστη
   * @param user Τα στοιχεία του χρήστη
   * @returns Observable για επιτυχία ή αποτυχία
   */
  registerUser(user: User): Observable<any> {
    return this.http.post(`${apiUrl}/register`, user);
  }

  /**
   * Ελέγχει αν το email υπάρχει ήδη
   * @param email Το email που θα ελεγχθεί
   * @returns Observable με μήνυμα επιτυχίας ή αποτυχίας
   */
  checkDuplicateEmail(email: string): Observable<any> {
    return this.http.get<{ message: string }>(`${apiUrl}/check_duplicate_email/${email}`);
  }

  /**
   * Κάνει logout τον χρήστη
   */
  logoutUser(): void {
    this.user.set(null);
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
    localStorage.removeItem('username');
    localStorage.removeItem('role');
    this.router.navigate(['login']);
  }

  /**
   * Ελέγχει αν ο χρήστης είναι logged in
   * @returns boolean
   */
  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }
}
