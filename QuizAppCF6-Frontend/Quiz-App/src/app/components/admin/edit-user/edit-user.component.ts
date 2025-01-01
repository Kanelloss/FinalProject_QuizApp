import { Component, inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { UserService } from '../../../shared/services/user.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatRadioModule } from '@angular/material/radio';

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
        Validators.minLength(8),
        Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$/),
      ]),
      email: new FormControl('', [Validators.required, Validators.email]),
      role: new FormControl('User', [Validators.required]),
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
          alert('User updated successfully!');
          this.router.navigate(['/admin/users']);
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
            alert('Failed to update user.');
          }
        },
      });
    }
  }
  

  onCancel() {
    this.router.navigate(['/admin/users']);
  }
}
