import { Component, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { QuizService } from '../../shared/services/quiz.service';

@Component({
  selector: 'app-quiz',
  standalone: true,
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.css'],
})
export class QuizComponent {
  quizService = inject(QuizService);
  route = inject(ActivatedRoute);
  router = inject(Router);

  quiz: any = null;
  currentIndex = 0;
  selectedAnswers: (string | null)[] = [];

  get currentQuestion() {
    return this.quiz?.questions[this.currentIndex];
  }

  async ngOnInit() {
    const quizId = this.route.snapshot.params['id'];
    this.quiz = (await this.quizService.startQuiz(quizId)).quiz;
    this.selectedAnswers = Array(this.quiz.questions.length).fill(null);
  }

  selectAnswer(option: string) {
    this.selectedAnswers[this.currentIndex] = option;
  }

  prevQuestion() {
    if (this.currentIndex > 0) {
      this.currentIndex--;
    }
  }

  nextQuestion() {
    if (this.currentIndex < this.quiz.questions.length - 1) {
      this.currentIndex++;
    }
  }
  
    submitQuiz() {
      const answers = this.selectedAnswers.map((selectedOption, index) => {
        if (!selectedOption) {
          throw new Error(`Question ${index + 1} has not been answered.`);
        }
        return {
          questionId: index,
          selectedOption: selectedOption,
        };
      });
    
      this.quizService.submitQuiz(this.quiz.quizId, { answers }).subscribe({
        next: (response) => {
          const score = response.result.score;
          const totalQuestions = response.result.totalQuestions;
          const questionResults = response.result.questionResults;
    
          console.log('Submission result:', response);
    
          // Redirect to Results Page
          this.router.navigate(['/results'], {
            state: {
              score: score,
              totalQuestions: totalQuestions,
              questionResults: questionResults,
              quizId: this.quiz.quizId, // Προσθήκη quizId
              questions: this.quiz.questions // Pass all questions here.
            },
          });
        },
        error: (error) => {
          console.error('Error submitting quiz:', error);
          alert('There was an error submitting your quiz. Please try again.');
        },
      });
    }
  }

