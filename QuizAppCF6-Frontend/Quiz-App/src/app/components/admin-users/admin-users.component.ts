import { Component, OnInit, inject } from '@angular/core';
import { UserService } from '../../shared/services/user.service';
import { MatTableModule } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog'; 
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';
import { AlertDialogComponent } from '../../shared/components/alert-dialog/alert-dialog.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-users',
  standalone: true,
  imports: [MatTableModule],
  templateUrl: './admin-users.component.html',
  styleUrls: ['./admin-users.component.css'],
})
export class AdminUsersComponent implements OnInit {
  userService = inject(UserService);
  dialog = inject(MatDialog);
  router = inject(Router);
  users: any[] = [];
  displayedColumns: string[] = ['id', 'username', 'email', 'role', 'actions'];

  ngOnInit() {
    this.fetchUsers();
  }

  fetchUsers() {
    this.userService.getAllUsers().subscribe({
      next: (users) => {
        this.users = users;
        console.log('Fetched users:', users);
      },
      error: (error) => {
        console.error('Error fetching users:', error);
      },
    });
  }

  editUser(userId: number): void {
    this.router.navigate(['/admin/users/edit', userId]);
  }

  deleteUser(userId: number): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: 'Delete User',
        message: 'Are you sure you want to delete this user?',
      },
    });
  
    dialogRef.afterClosed().subscribe((confirmed) => {
      if (confirmed) {
        this.userService.deleteUser(userId).subscribe({
          next: () => {
            // Open the alert dialog for success
            this.dialog.open(AlertDialogComponent, {
              data: {
                title: 'Success',
                message: 'User deleted successfully.',
              },
            });
            this.fetchUsers(); // Refresh the users list
          },
          error: (error) => {
            console.error('Error deleting user:', error);
            // Open the alert dialog for error
            this.dialog.open(AlertDialogComponent, {
              data: {
                title: 'Error',
                message: 'Failed to delete user. Please try again.',
              },
            });
          },
        });
      }
    });
  }

  navigateToRegister() {
    this.router.navigate(['/register']); // Navigate to Register Page
  }
}
