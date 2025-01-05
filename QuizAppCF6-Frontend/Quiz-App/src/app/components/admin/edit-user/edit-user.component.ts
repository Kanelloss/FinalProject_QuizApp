import { Component, inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { UserService } from '../../../shared/services/user.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatRadioModule } from '@angular/material/radio';
import { AlertDialogComponent } from '../../../shared/components/alert-dialog/alert-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../../shared/components/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-edit-user',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatRadioModule,
  ],
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css'],
})
export class EditUserComponent implements OnInit {
  userService = inject(UserService);
  router = inject(Router);
  route = inject(ActivatedRoute);
  dialog = inject(MatDialog);

  form!: FormGroup;

  ngOnInit() {
    this.route.params.subscribe((params) => {
      const userId = +params['id'];
      this.initializeForm();
      this.loadUser(userId);
    });
  }

  initializeForm() {
    this.form = new FormGroup({
      username: new FormControl('', [Validators.required, Validators.minLength(2)]),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$/),
      ]),
      email: new FormControl('', [Validators.required, Validators.email]),
      userRole: new FormControl('User', [Validators.required]),
    });
  }

  loadUser(userId: number) {
    this.userService.getUserById(userId).subscribe({
      next: (user) => {
        this.form.patchValue({
          username: user.username,
          email: user.email,
          role: user.userRole,
        });
      },
      error: () => {
        alert('Failed to load user details.');
        this.router.navigate(['/admin/users']);
      },
    });
  }

   onSubmit() {
    if (this.form.valid) {
      const formValue = this.form.value;

      this.userService.updateUser(this.route.snapshot.params['id'], formValue).subscribe({
        next: () => {
          this.showSuccessDialog('User updated successfully!');
          setTimeout(() => {
            this.router.navigate(['/admin/users']);
          }, 900);
        },
        error: (error) => {
          if (error.error && error.error.errors) {
            const validationErrors = error.error.errors;
            for (const key in validationErrors) {
              if (this.form.controls[key]) {
                this.form.controls[key].setErrors({ backend: validationErrors[key] });
              }
            }
          } else {
            this.showErrorDialog('Failed to update user. Please try again.');
          }
        },
      });
    }
  }

  showSuccessDialog(message: string) {
    this.dialog.open(AlertDialogComponent, {
      data: {
        title: 'Success',
        message: message,
      },
    });
  }

  showErrorDialog(message: string) {
    this.dialog.open(AlertDialogComponent, {
      data: {
        title: 'Error',
        message: message,
      },
    });
  }
  

  onCancel() {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: {
          title: 'Cancel Editing',
          message: 'Any changes you made will not be saved. Are you sure you want to cancel?',
      },
  });

  dialogRef.afterClosed().subscribe((confirmed) => {
      if (confirmed) {
          this.router.navigate(['/admin/users']); // Redirect back to user management page
      }
  });
  }
}
