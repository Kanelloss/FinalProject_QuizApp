import { Component, inject } from '@angular/core';
import { UserService } from '../../shared/services/user.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  userService = inject(UserService);

  logout(): void {
    this.userService.logoutUser();
  }
}
