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

 
  registerUser(user: User) {
    return this.http.post(`${apiUrl}/signup`, user);
  }

  getUserRole(): string | null {
    return localStorage.getItem('role');
  }

  getCurrentUserId(): number {
    const userId = localStorage.getItem('userId');
    return userId ? +userId : 0; // Επιστροφή 0 αν δεν υπάρχει userId
  }
  
  isAdmin(): boolean {
    return this.getUserRole() === 'Admin';
  }

  logoutUser(): void {
    this.user.set(null);
    localStorage.removeItem('access_token');
    localStorage.removeItem('userId');
    localStorage.removeItem('username');
    localStorage.removeItem('role');
    this.router.navigate(['login']);
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('access_token');
  }

   getAllUsers(): Observable<any[]> {
    return this.http.get<any[]>(`${apiUrl}/getall`);
  }

  deleteUser(userId: number): Observable<any> {
    return this.http.delete(`${apiUrl}/${userId}`);
  }

  getUserById(id: number): Observable<any> {
    return this.http.get<any>(`${apiUrl}/${id}`);
  }

  updateUser(id: number, user: any) {
    console.log('Data sent to backend:', user); // Debug τα δεδομένα
    return this.http.put(`${apiUrl}/${id}`, user);
  }
  
}
