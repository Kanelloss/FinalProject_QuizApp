import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterModule, 
    MatIconModule,
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  categories = [
    { name: 'General Knowledge', icon: 'emoji_objects' },
    { name: 'Dictionary', icon: 'menu_book' },
    { name: 'Entertainment', icon: 'theater_comedy' },
    { name: 'History', icon: 'history_edu' },
    { name: 'Food & Drink', icon: 'restaurant_menu' },
    { name: 'Geography', icon: 'public' },
    { name: 'Science & Nature', icon: 'science' },
  ];
  
}
