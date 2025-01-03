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
  openDropdownId: number | null = null; // Track which dropdown is open


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

  toggleEditDropdown(quizId: number) {
    if (this.openDropdownId === quizId) {
      this.openDropdownId = null; // Close dropdown if it's already open
    } else {
      this.openDropdownId = quizId; // Open the dropdown
    }
  }

  editDetails(quizId: number) {
    console.log('Edit Details for Quiz ID:', quizId);
    this.router.navigate([`/admin/quizzes/${quizId}/edit-details`]);
  }

  editQuestions(quizId: number) {
    console.log('Edit Questions for Quiz ID:', quizId);
    this.router.navigate([`/admin/quizzes/${quizId}/edit-questions`]);
  }


  isDropdownOpen(quizId: number): boolean {
    return this.openDropdownId === quizId;
  }

  navigateToAddQuiz() {
    this.router.navigate(['/admin/quizzes/add']); // Navigate to the Add Quiz page
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
