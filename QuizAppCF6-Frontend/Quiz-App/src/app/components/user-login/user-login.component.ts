import { Component, inject } from '@angular/core';
import { FormControl, FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { UserService } from '../../shared/services/user.service';
import { Credentials } from '../../shared/interfaces/user';
import { Router, RouterModule } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { LoggedInUser } from '../../shared/interfaces/user';

@Component({
  selector: 'app-user-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    RouterModule,
  ],
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent {
  hide = true;
  userService = inject(UserService);
  router = inject(Router);
  invalidLogin = false;

   errorMessage: string | null = null; // Κεντρικό μήνυμα λαθών
   successMessage: string | null = null; // Για μηνύματα επιτυχίας

  form = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required])
  });

  onSubmit() {
    const credentials = this.form.value as Credentials;
    this.userService.loginUser(credentials).subscribe({
      next: (response) => {
        const access_token = response.token;
        console.log('Login successful!');
        localStorage.setItem('access_token', access_token);
  
        const decodedToken: LoggedInUser | any = jwtDecode(access_token);
  
        // Extract relevant fields
        const userId = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
        const username = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
        const email = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"];
        const role = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
        
        // Log
        console.log(`Decoded Info: ID: ${userId}, Username: ${username}, Email: ${email}, Role: ${role}`);
  
        // Store useful info in localStorage
        localStorage.setItem('userId', userId);
        localStorage.setItem('username', username);
        localStorage.setItem('role', role);
  
        this.successMessage = 'Successful login!';
        this.errorMessage = null;
  
        setTimeout(() => {
          this.router.navigate(['home']).then((success) => {
            console.log('Redirected to home successfully:', success);
          }).catch((err) => {
            console.error('Failed to redirect:', err);
          });
        }, 1000);
      },
      error: (error) => {
        console.error('Login failed:', error);
  
        // Check for error code: 400 & 401
        if (error.status === 401 || error.status === 400) {
          this.errorMessage = error.error?.message || 'Invalid credentials.';
        } else {
          this.errorMessage = 'Unexpected error occurred.';
        }
  
        // Delete success message
        this.successMessage = null;
      },
    });
  }
}