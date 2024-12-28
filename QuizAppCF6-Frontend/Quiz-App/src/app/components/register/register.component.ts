import { Component, inject } from '@angular/core';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { UserService } from '../../shared/services/user.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatRadioModule } from '@angular/material/radio';
import { User } from '../../shared/interfaces/user';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatRadioModule,
  ],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  userService = inject(UserService);
  router = inject(Router);

  successMessage = '';
  errorMessage = '';

  // Form group for the register form
  form = new FormGroup({
    username: new FormControl('', [Validators.required, Validators.minLength(2)]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
      Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$/),
    ]),
    email: new FormControl('', [Validators.required, Validators.email]),
    role: new FormControl('User', [Validators.required]), // Default: 'User'
  });

  onSubmit() {
    if (this.form.valid) {
      const formValue = this.form.value;

      // Δημιουργούμε το αντικείμενο User
      const user: User = {
        username: formValue.username as string,
        password: formValue.password as string,
        email: formValue.email as string,
        UserRole: formValue.role as 'Admin' | 'User',
      };

      console.log('Sending user object to backend:', user);

      this.userService.registerUser(user).subscribe({
        next: () => {
          this.successMessage = 'Registration successful! Redirecting to login...';
          this.errorMessage = '';
          setTimeout(() => {
            this.router.navigate(['/login']);
          }, 1000);
        },
        error: (error) => {
          console.error('Registration failed:', error.error?.message || 'Unexpected error occurred.');
          this.errorMessage = error.error?.message || 'Unexpected error occurred.';
          this.successMessage = '';
        },
      });
    }
  }
}
