import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { QuizService } from '../../shared/services/quiz.service';

@Component({
  selector: 'app-quiz',
  standalone: true,
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.css'],
})
export class QuizComponent {
  private quizService = inject(QuizService);
  private route = inject(ActivatedRoute);

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

  async submitQuiz() {
    const answers = this.selectedAnswers.map((selectedOption, index) => {
      if (!selectedOption) {
        throw new Error(`Question ${index + 1} has not been answered.`);
      }
      return {
        questionId: index,
        selectedOption: selectedOption,
      };
    });
  
    try {
      const result = await this.quizService.submitQuiz(this.quiz.id, answers);
      alert(`Quiz completed! Your score: ${result.score}`);
    } catch (error) {
      console.error('Error submitting quiz:', error);
      alert('There was an error submitting your quiz. Please try again.');
    }
  }
}
