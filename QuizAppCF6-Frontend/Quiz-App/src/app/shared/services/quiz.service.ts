import { inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { HttpErrorResponse } from '@angular/common/http';
import { firstValueFrom } from 'rxjs/internal/firstValueFrom';
import { QuizResult } from '../interfaces/quizresult';
import { Observable } from 'rxjs';

const apiUrl = `${environment.apiUrl}/Quiz`; // backend URL

@Injectable({
  providedIn: 'root',
})
export class QuizService {
  private http = inject(HttpClient);

  categories = signal<{ id: number; name: string }[]>([]);
  highScores = signal<{ username: string; score: number; achievedAt: string }[]>([]);
  isLoading = signal(false);

  // Fetch all available quizzes to add in the leaderboards table.
  async fetchAvailableQuizzes() {
    console.log(`[QuizService] Fetching available quizzes...`);
    const availableQuizzes = [];
    let quizzesNumber = 10;   // Manually change if a quiz is added/deleted to include in leaderboards.

    for (let quizId = 1; quizId <= quizzesNumber; quizId++) {
      try {
        const response = await firstValueFrom(
          this.http.get<{
            quizTitle: string;
            highScores: { username: string; score: number; achievedAt: string }[];
          }>(`${apiUrl}/${quizId}/alltimehighscores`)
        );

        if (response?.quizTitle) {
          availableQuizzes.push({ id: quizId, name: response.quizTitle });
          console.log(`[QuizService] Quiz found: ${response.quizTitle}`);
        }
      } catch (error) {
        console.warn(`[QuizService] No quiz found for ID: ${quizId}`);
      }
    }

    this.categories.set(availableQuizzes);
    console.log(`[QuizService] Final categories:`, this.categories());
  }

  // Fetch a quiz's data by its id.
  async fetchQuizData(quizId: number) {
    this.isLoading.set(true);
    console.log(`[QuizService] Fetching quiz data for ID: ${quizId}`);
    try {
      const response = await firstValueFrom(
        this.http.get<{
          quizTitle: string;
          highScores: { username: string; score: number; achievedAt: string }[];
        }>(`${apiUrl}/${quizId}/alltimehighscores`)
      );

      this.highScores.set(response.highScores);
      console.log(`[QuizService] Response from backend:`, response);
    } catch (error) {
      console.error(`[QuizService] Error fetching quiz data:`, error);
    } finally {
      this.isLoading.set(false);
      console.log(`[QuizService] Fetching completed.`);
    }
  }

  // Start a quiz (using id)
  startQuiz(quizId: number) {
    return firstValueFrom(
      this.http.get<{ message: string; quiz: any }>(`${apiUrl}/${quizId}/start`, {})
    );
  }

    // Submit a quiz (using id)
  submitQuiz(quizId: number, answers: any) {
    return this.http.post<{ message: string; result: { score: number; correctAnswers: number; totalQuestions: number; questionResults: any[] } }>(
      `${apiUrl}/${quizId}/submit`,
      answers
    );
  }

  // Get all quizzes and their descriptions
  getAllQuizzes(): Observable<any[]> {
    return this.http.get<any[]>(`${apiUrl}/getall`);
  }

  // Delete a quiz by id, admin only.
  deleteQuiz(quizId: number): Observable<any> {
    return this.http.delete(`${apiUrl}/${quizId}`);
  }

  // Add new quiz, admin only.
  addQuiz(quiz: { title: string; description: string }): Observable<any> {
    return this.http.post<any>(`${apiUrl}`, quiz);
  }

  // Get a quiz by its id.
  getQuizById(id: number): Observable<any> {
    return this.http.get<any>(`${apiUrl}/${id}`);
  }

  // Update quiz details using id, admin only.
  updateQuiz(id: number, quizData: any): Observable<any> {
    return this.http.put<any>(`${apiUrl}/${id}`, quizData);
  }

  // Update a question using id, admin only.
  updateQuestion(questionId: number, questionData: any): Observable<any> {
    return this.http.put<any>(`${apiUrl}/questions/${questionId}`, questionData);
  }

  // Get a specific quiz's question using index (starts with 0)
  getQuestionByIndex(quizId: number, questionIndex: number): Observable<any> {
    return this.http.get<any>(`${apiUrl}/${quizId}/questions/${questionIndex}`);
  }
}