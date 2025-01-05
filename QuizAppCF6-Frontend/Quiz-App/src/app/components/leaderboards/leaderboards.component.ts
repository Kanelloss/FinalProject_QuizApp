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
  quizService = inject(QuizService);
  selectedCategory = 0;

  async ngOnInit() {
    console.log('[LeaderboardsComponent] Fetching available quizzes...');
    await this.quizService.fetchAvailableQuizzes();

    if (this.quizService.categories().length > 0) {
      this.onCategoryChange(this.quizService.categories()[0].id); // Default = First category
    } else {
      console.warn('[LeaderboardsComponent] No categories available.');
    }
  }

  async onCategoryChange(categoryId: number) {
    this.selectedCategory = categoryId;
    await this.quizService.fetchQuizData(categoryId);
  }

  // Format date to YYYY-MM-DD HH:mm:ss
  formatDate(dateString: string): string {
    const date = new Date(dateString);
    return date.toISOString().slice(0, 19).replace('T', ' ');
  }
}
