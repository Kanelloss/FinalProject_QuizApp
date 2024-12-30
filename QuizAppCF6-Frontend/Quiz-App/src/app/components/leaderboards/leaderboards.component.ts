import { Component, inject } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
import { QuizService } from '../../shared/services/quiz.service';

@Component({
  selector: 'app-leaderboards',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatSelectModule,
    MatOptionModule,
  ],
  templateUrl: './leaderboards.component.html',
  styleUrls: ['./leaderboards.component.css'],
})
export class LeaderboardsComponent {
  quizService = inject(QuizService); // Injected QuizService
  selectedCategory = 0;

  async ngOnInit() {
    console.log('[LeaderboardsComponent] Fetching available quizzes...');
    await this.quizService.fetchAvailableQuizzes(); // Ανακτά τα διαθέσιμα quizzes

     // Debugging τις κατηγορίες
  console.log('[LeaderboardsComponent] Available categories:', this.quizService.categories());


    if (this.quizService.categories().length > 0) {
      console.log('[LeaderboardsComponent] Auto-selecting first category:', this.quizService.categories()[0].id);
      this.onCategoryChange(this.quizService.categories()[0].id); // Επιλέγει την πρώτη κατηγορία
    } else {
      console.warn('[LeaderboardsComponent] No categories available.');
    }
  }

  async onCategoryChange(categoryId: number) {
    console.log('[LeaderboardsComponent] Category changed to:', categoryId);
    this.selectedCategory = categoryId;

    // Ανακτά τα high scores για την επιλεγμένη κατηγορία
    await this.quizService.fetchQuizData(categoryId);

    console.log('[LeaderboardsComponent] High scores updated:', this.quizService.highScores());
  }

  // Μέθοδος για τη μορφοποίηση της ημερομηνίας
  formatDate(dateString: string): string {
    const date = new Date(dateString);
    return date.toISOString().slice(0, 19).replace('T', ' '); // Παίρνουμε μόνο μέχρι τα δευτερόλεπτα
  }
}
