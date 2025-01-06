import { Component, inject } from '@angular/core';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { UserService } from '../../shared/services/user.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatRadioModule } from '@angular/material/radio';
import { User } from '../../shared/interfaces/user';
import { MatDialog } from '@angular/material/dialog';
import { AlertDialogComponent } from '../../shared/components/alert-dialog/alert-dialog.component';
import { ConfirmDialogComponent } from '../../shared/components/confirm-dialog/confirm-dialog.component';

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
  dialog = inject(MatDialog);

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
  
      const user: User = {
        username: formValue.username as string,
        password: formValue.password as string,
        email: formValue.email as string,
        UserRole: formValue.role as 'Admin' | 'User',
      };
  
      this.userService.registerUser(user).subscribe({
        next: () => {
          this.dialog.open(AlertDialogComponent, {
            data: { message: 'User registered successfully' },
          });
          setTimeout(() => {
            this.router.navigate(['/login']);
          }, 500);
        },
        error: (error) => {
          if (error.error?.message === 'Username is already taken.') {
            this.form.get('username')?.setErrors({ duplicate: 'Username is already taken.' });
          } else if (error.error?.message === 'Email is already taken.') {
            this.form.get('email')?.setErrors({ duplicate: 'Email is already taken.' });
          } else {
            this.dialog.open(AlertDialogComponent, {
              data: { message: 'Failed to register user, please try again.' },
            });
          }
        },
      });
    }
  }
  
  onCancel() {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: {
          title: 'Cancel Registration',
          message: 'Any changes you made will not be saved. Are you sure you want to cancel?',
      },
  });

  dialogRef.afterClosed().subscribe((confirmed) => {
      if (confirmed) {
          this.router.navigate(['/admin/users']);
      }
  });
  }
}
