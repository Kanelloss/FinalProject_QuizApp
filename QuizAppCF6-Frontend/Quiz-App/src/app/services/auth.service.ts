import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User, Credentials, LoggedInUser } from '../interfaces/user';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:5000/api/user';

  constructor(private http: HttpClient) {}

  login(credentials: Credentials): Observable<{ token: string; user: LoggedInUser }> {
    return this.http.post<{ token: string; user: LoggedInUser }>(`${this.apiUrl}/login`, credentials);
  }

  register(user: User): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(`${this.apiUrl}/register`, user);
  }

  logout(): void {
    localStorage.removeItem('token');
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }
}
