import { Component, OnInit, inject } from '@angular/core';
import { QuizService } from '../../shared/services/quiz.service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';
import { AlertDialogComponent } from '../../shared/components/alert-dialog/alert-dialog.component';

@Component({
  selector: 'app-admin-quizzes',
  standalone: true,
  templateUrl: './admin-quizzes.component.html',
  styleUrls: ['./admin-quizzes.component.css'],
})
export class AdminQuizzesComponent implements OnInit {
  quizService = inject(QuizService);
  router = inject(Router);
  dialog = inject(MatDialog);
  quizzes: any[] = [];

  ngOnInit() {
    this.loadQuizzes();
  }

  loadQuizzes() {
    this.quizService.getAllQuizzes().subscribe({
      next: (data) => {
        this.quizzes = data;
      },
      error: (error) => {
        console.error('Error fetching quizzes:', error);
        alert('Failed to load quizzes. Please try again later.');
      },
    });
  }

  navigateToAddQuiz() {
    this.router.navigate(['/admin/quizzes/add']); // Navigate to the Add Quiz page
  }

  editQuiz(quizId: number) {
    console.log('Editing quiz with ID:', quizId);
    // Redirect to Edit Quiz Page
  }

  deleteQuiz(quizId: number) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: 'Delete Quiz Confirmation',
        message: `Are you sure you want to delete the quiz with ID ${quizId}?`,
      },
    });
  
    dialogRef.afterClosed().subscribe((confirmed) => {
      if (confirmed) {
        this.quizService.deleteQuiz(quizId).subscribe({
          next: () => {
            const successDialog = this.dialog.open(AlertDialogComponent, {
              data: { message: 'Quiz deleted successfully!' },
            });
            setTimeout(() => successDialog.close(), 1500);
            this.loadQuizzes(); // Reload the quizzes after deletion
          },
          error: (error) => {
            console.error('Error deleting quiz:', error);
            const errorDialog = this.dialog.open(AlertDialogComponent, {
              data: { message: 'Failed to delete quiz. Please try again.' },
            });
            setTimeout(() => errorDialog.close(), 1500);
          },
        });
      }
    });
  }
}
