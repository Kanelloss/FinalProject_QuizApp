import { Inject, inject, Injectable, signal, effect } from '@angular/core'; // signal & effect μαζί.
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Credentials, User, LoggedInUser } from '../interfaces/user';
import { environment } from '../../../environments/environment';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';

const apiUrl = `${environment.apiUrl}/User`; // backend URL

@Injectable({
  providedIn: 'root'
})
export class UserService {
  http: HttpClient = inject(HttpClient);
  router = inject(Router);
  
  user = signal<LoggedInUser | null>(null)

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

  // Extract user details from token
   private setUserFromToken(token: string) {
    const decodedToken: any = jwtDecode(token);
    this.user.set({
      id: decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'],
      username: decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
      email: decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
      role: decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
    });
   }

   // Check if JWT has expired
   isTokenExpired(): boolean {
    const token = localStorage.getItem('access_token');
    if (!token) return true;
  
    try {
      const decodedToken: any = jwtDecode(token);
      const expirationTime = decodedToken.exp * 1000; // exp in seconds
      return Date.now() > expirationTime;
    } catch (error) {
      console.error('Invalid token:', error);
      return true;
    }
  }

   // Login User
  loginUser(credentials: Credentials) {
    return this.http.post<{token:string}>(`${apiUrl}/login`, credentials)
  }
  
  // Register a user
  registerUser(user: User) {
    return this.http.post(`${apiUrl}/signup`, user);
  }

  // Get user role for authorization purposes
  getUserRole(): string | null {
    return localStorage.getItem('role');
  }

  // Checks if a user is admin
  isAdmin(): boolean {
    return this.getUserRole() === 'Admin';
  }

  // Get logged in user's id
  getCurrentUserId(): number {
    const userId = localStorage.getItem('userId');
    return userId ? +userId : 0; // Επιστροφή 0 αν δεν υπάρχει userId
  }
  
  // Logout user and delete all saved user data from local storage
  logoutUser(): void {
    this.user.set(null);
    localStorage.removeItem('access_token');
    localStorage.removeItem('userId');
    localStorage.removeItem('username');
    localStorage.removeItem('role');
    this.router.navigate(['login']);
  }

  // Check if a user is logged in
  isLoggedIn(): boolean {
    return !!localStorage.getItem('access_token');
  }

  // Get all users, admin only
   getAllUsers(): Observable<any[]> {
    return this.http.get<any[]>(`${apiUrl}/getall`);
  }

  // Delete a user by id, admin only 
  deleteUser(userId: number): Observable<any> {
    return this.http.delete(`${apiUrl}/${userId}`);
  }

  // Get a user by id, admin only (user can get only his account details, not other users')
  getUserById(id: number): Observable<any> {
    return this.http.get<any>(`${apiUrl}/${id}`);
  }

  // Update a user by id, admin only
  updateUser(id: number, user: any) {
    console.log('Data sent to backend:', user); // Debug τα δεδομένα
    return this.http.put(`${apiUrl}/${id}`, user);
  }
}
