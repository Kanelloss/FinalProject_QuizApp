import { Component, HostListener, inject } from '@angular/core';
import { UserService } from '../../shared/services/user.service';
import { RouterModule, Router } from '@angular/router';
import { ConfirmDialogComponent } from '../../shared/components/confirm-dialog/confirm-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent {
  menuOpen = false;
  adminDropdownOpen = false;
  accountDropdownOpen = false;
  userService = inject(UserService);
  router = inject(Router);
  dialog = inject(MatDialog);

  toggleMenu() {
    this.menuOpen = !this.menuOpen;
  }

  toggleAccountDropdown() {
    this.accountDropdownOpen = !this.accountDropdownOpen;
    if (this.accountDropdownOpen) {
      this.adminDropdownOpen = false;
    }
  }

  toggleAdminDropdown() {
    this.adminDropdownOpen = !this.adminDropdownOpen;
    if (this.adminDropdownOpen) {
      this.accountDropdownOpen = false;
    }
  }

  editAccount() {
    const userId = this.userService.getCurrentUserId();
    if (userId) {
      this.router.navigate([`/admin/users/edit/${userId}`]);
      this.accountDropdownOpen = false;
    }
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

  isLoggedIn(): boolean {
    return this.userService.isLoggedIn();
  }

  getUsername(): string {
    return localStorage.getItem('username') || 'Guest';
  }

  isAdmin(): boolean {
    return this.userService.isAdmin();
  }

  @HostListener('document:click', ['$event'])
  closeDropdownsOnOutsideClick(event: MouseEvent) {
    const target = event.target as HTMLElement;
    if (
      !target.closest('.dropdown') &&
      !target.closest('.hamburger') &&
      !target.closest('.nav-links')
    ) {
      this.accountDropdownOpen = false;
      this.adminDropdownOpen = false;
    }
  }

  @HostListener('window:resize', ['$event'])
  closeMenuOnResize(event: Event) {
    const width = (event.target as Window).innerWidth;
    if (width > 768) {
      this.menuOpen = false;
    }
  }
  
  navigateAndCloseDropdown(route: string) {
    this.router.navigate([route]);
    this.adminDropdownOpen = false;
  }
  


}
