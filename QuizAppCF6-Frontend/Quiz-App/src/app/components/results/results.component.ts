import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-results',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './results.component.html',
  styleUrls: ['./results.component.css'],
})
export class ResultsComponent {
  score: number | null = null;
  totalQuestions: number | null = null;
  questionResults: any[] = [];
  quizId: number | null = null;
  questions: any[] = [];
  hasResults = false;

  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    const state = navigation?.extras.state as {
      score: number;
      totalQuestions: number;
      questionResults: any[];
      quizId: number;
      questions: any[];
    };

    if (state) {
      this.score = state.score;
      this.totalQuestions = state.totalQuestions;
      this.questionResults = state.questionResults;
      this.quizId = state.quizId;
      this.questions = state?.questions || [];
      this.hasResults = true;
    } else {
      // Redirect to home if there are no data
      this.router.navigate(['/home']);
    }
  }
}
