import { Component, inject } from '@angular/core';
import { UserService } from '../../shared/services/user.service';
import { RouterModule } from '@angular/router';

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
  user = this.userService.user;

toggleMenu() {
  this.menuOpen = !this.menuOpen;
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


}
