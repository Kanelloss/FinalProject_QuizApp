import { Component, HostListener, inject } from '@angular/core';
import { UserService } from '../../shared/services/user.service';
import { RouterModule, Router } from '@angular/router';
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  menuOpen = false;
  userService = inject(UserService);
  router = inject(Router);
  dialog = inject(MatDialog);
  user = this.userService.user;

toggleMenu() {
  this.menuOpen = !this.menuOpen;
}

@HostListener('window:resize', ['$event'])
onResize(event: Event) {
  const width = (event.target as Window).innerWidth;
  if (width > 768 && this.menuOpen) {
    this.menuOpen = false; // Close menu if screen is resized to larger size
  }
}

  isLoggedIn(): boolean {
    return this.userService.isLoggedIn();
  }

  getUsername(): string {
    return localStorage.getItem('username') || 'Guest';
  }

  logout(): void {
    this.userService.logoutUser();
  }

  confirmLogout() {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: 'Logout Confirmation:',
        message: 'Are you sure you want to log out?',
      },
    });

    dialogRef.afterClosed().subscribe((confirmed) => {
      if (confirmed) {
        this.userService.logoutUser();
        this.router.navigate(['/welcome']);
      }
    });
  }

  isAdmin(): boolean {
    return this.userService.isAdmin();
  }
  


}
