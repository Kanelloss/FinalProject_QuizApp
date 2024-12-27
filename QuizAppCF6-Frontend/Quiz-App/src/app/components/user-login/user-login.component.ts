import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { UserService } from '../../shared/services/user.service';
import { Credentials } from '../../shared/interfaces/user';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { LoggedInUser } from '../../shared/interfaces/user';

@Component({
  selector: 'app-user-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent {
  userService = inject(UserService);
  router = inject(Router);
  invalidLogin = false;

  // Δημιουργία Reactive Form
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
        console.log(access_token);

        const decodedToken: LoggedInUser | any = jwtDecode(access_token);

      // Extract relevant fields
      const userId = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
      const username = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
      const email = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"];
      const role = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
      // Log
      console.log(`Decoded Info: ID: ${userId}, Username: ${username}, Email: ${email}, Role: ${role}`);

      // store useful info in localStorage
      localStorage.setItem('userId', userId);
      localStorage.setItem('username', username);
      localStorage.setItem('role', role);
      
        this.router.navigate(['home']);
        
      },
      error: (error) => {
        console.error('Login failed:', error.error?.message || 'Unexpected error');
        this.invalidLogin = true;
      }
    });
  }
  
  
}
