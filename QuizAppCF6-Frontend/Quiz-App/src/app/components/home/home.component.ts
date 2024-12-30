import { Component, inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterModule, 
    MatIconModule,
    MatDialogModule
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  categories = [
    { id: 1, name: 'Science', icon: 'science' },
    { id: 2, name: 'History', icon: 'history_edu' },
    { id: 3, name: 'Geography', icon: 'public' },
    { id: 4, name: 'Mathematics', icon: 'calculate' },
    { id: 5, name: 'Technology', icon: 'memory' },
    { id: 6, name: 'Gaming', icon: 'sports_esports' },
    { id: 7, name: 'Dictionary', icon: 'menu_book' },
    { id: 8, name: 'Entertainment', icon: 'theater_comedy' },
    { id: 9, name: 'Food & Drink', icon: 'restaurant_menu' },
  ];

  dialog = inject(MatDialog);
  router = inject(Router);

  async onCategoryClick(category: any) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: {
        title: `Start a New ${category.name} Quiz?`,
        message: 'Do you want to start a new quiz for this category?',
      },
    });

    const result = await firstValueFrom(dialogRef.afterClosed()); // Χρήση firstValueFrom αντί για toPromise
    if (result) {
      this.router.navigate([`/quiz/${category.id}/start`]); // Πλοήγηση στο σωστό endpoint
    }
  }
}
