import { Component, inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { UserService } from '../../shared/services/user.service';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-welcome',
  standalone: true,
  imports: [RouterModule, MatButtonModule],
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.css'],
})
export class WelcomeComponent {
  userService = inject(UserService);
  router = inject(Router);

  navigateTo(destination: string) {
    this.router.navigate([destination]);
  }

  isLoggedIn(): boolean {
    return this.userService.isLoggedIn();
  }
}
