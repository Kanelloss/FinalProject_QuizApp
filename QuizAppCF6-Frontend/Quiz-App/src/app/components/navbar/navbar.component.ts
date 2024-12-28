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

toggleMenu() {
  this.menuOpen = !this.menuOpen;
}

  userService = inject(UserService);

  logout(): void {
    this.userService.logoutUser();
  }
}
