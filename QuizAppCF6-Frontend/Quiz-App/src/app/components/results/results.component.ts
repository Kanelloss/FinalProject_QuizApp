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
  questions: any[] = []; // Add this to the component
  hasResults = false;

  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    const state = navigation?.extras.state as {
      score: number;
      totalQuestions: number;
      questionResults: any[];
      quizId: number;
      questions: any[]; // Add this to the component
    };

    if (state) {
      this.score = state.score;
      this.totalQuestions = state.totalQuestions;
      this.questionResults = state.questionResults;
      this.quizId = state.quizId; // Αναθέτουμε το quizId
      this.questions = state?.questions || []; // Add this line.
      this.hasResults = true;
    } else {
      // Redirect back to home if no data
      this.router.navigate(['/home']);
    }
  }
}
