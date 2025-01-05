import { Component, OnInit, inject } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormControl,
  FormArray,
  Validators,
  ReactiveFormsModule,
  AbstractControl,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { QuizService } from '../../../shared/services/quiz.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NgFor, NgIf } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { AlertDialogComponent } from '../../../shared/components/alert-dialog/alert-dialog.component';
import { ConfirmDialogComponent } from '../../../shared/components/confirm-dialog/confirm-dialog.component';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-edit-quiz',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    NgFor,
    NgIf,
  ],
  templateUrl: './edit-quiz.component.html',
  styleUrls: ['./edit-quiz.component.css'],
})
export class EditQuizComponent implements OnInit {
  form!: FormGroup;
  quizId!: number;
  quizService = inject(QuizService);
  router = inject(Router);
  route = inject(ActivatedRoute);
  fb = inject(FormBuilder);
  dialog = inject(MatDialog);

  ngOnInit() {
    this.initializeForm();

    // Fetch quiz ID from route and load quiz
    this.route.params.subscribe((params) => {
      this.quizId = +params['id'];
      this.loadQuiz(this.quizId);
    });
  }

  initializeForm() {
    this.form = this.fb.group({
      title: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required),
      questions: this.fb.array([]),
    });
  }

  getQuestionsFormArray(): FormArray {
    return this.form.get('questions') as FormArray;
  }

  createQuestionGroup(): FormGroup {
    return this.fb.group({
      id: new FormControl(''),
      text: new FormControl('', Validators.required),
      options: this.fb.array(
        Array(4).fill(null).map(() => new FormControl('', Validators.required))
      ),
      correctAnswer: new FormControl('', Validators.required),
      category: new FormControl('', Validators.required),
    });
  }
  

  getOptionsFormArray(question: AbstractControl): FormArray {
    return question.get('options') as FormArray;
  }

  loadQuiz(id: number) {
    this.quizService.getQuizById(id).subscribe({
      next: (quiz) => {
        this.form.patchValue({
          title: quiz.title,
          description: quiz.description,
        });
  
        this.getQuestionsFormArray().clear();
  
        quiz.questions.forEach((question: any, index: number) => {
          const questionGroup = this.createQuestionGroup();
          this.quizService.getQuestionByIndex(id, index).subscribe({
            next: (detailedQuestion) => {
              questionGroup.patchValue({
                id: detailedQuestion.id,
                text: detailedQuestion.text,
                options: detailedQuestion.options,
                correctAnswer: detailedQuestion.correctAnswer,
                category: detailedQuestion.category,
              });
              this.getQuestionsFormArray().push(questionGroup);
            },
            error: (error) => {
              console.error(`Error fetching question ${index + 1}:`, error);
            },
          });
        });
      },
      error: (error) => {
        console.error('Error fetching quiz:', error);
        alert('Failed to load quiz details. Please try again later.');
      },
    });
  }

  cancelEdit() {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
        data: {
            title: 'Cancel Editing',
            message: 'Any changes you made will not be saved. Are you sure you want to cancel?',
        },
    });

    dialogRef.afterClosed().subscribe((confirmed) => {
        if (confirmed) {
            this.router.navigate(['/admin/quizzes']); // Redirect back to quiz management page
        }
    });
}

  updateQuiz() {
    if (this.form.invalid) {
      alert('Please fill in all required fields.');
      return;
    }
  
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: 'Submit Quiz Changes',
        message: 'Are you sure you want to update the quiz and its questions?',
      },
    });
  
    dialogRef.afterClosed().subscribe((confirmed) => {
      if (confirmed) {
        const updatedQuiz = {
          title: this.form.get('title')?.value,
          description: this.form.get('description')?.value,
        };
  
        // Update Quiz Details
        const updateQuiz$ = this.quizService.updateQuiz(this.quizId, updatedQuiz);
  
        // Update Questions
        const questionUpdates$ = this.getQuestionsFormArray().controls.map((question, index) => {
          const questionData = {
            id: question.value.id,
            text: question.value.text,
            options: question.value.options,
            correctAnswer: question.value.correctAnswer,
            category: question.value.category,
          };
          return this.quizService.updateQuestion(questionData.id, questionData);
        });
  
        const allUpdates$ = forkJoin([updateQuiz$, ...questionUpdates$]);
  
        allUpdates$.subscribe({
          next: () => {
            const successDialog = this.dialog.open(AlertDialogComponent, {
              data: { message: 'Quiz and questions updated successfully!' },
            });
  
            setTimeout(() => {
              successDialog.close();
              this.router.navigate(['/admin/quizzes']);
            }, 900);
          },
          error: (error) => {
            console.error('Error updating quiz or questions:', error);
            this.dialog.open(AlertDialogComponent, {
              data: { message: 'Failed to update quiz. Please try again.' },
            });
          },
        });
      }
    });
  }
}  