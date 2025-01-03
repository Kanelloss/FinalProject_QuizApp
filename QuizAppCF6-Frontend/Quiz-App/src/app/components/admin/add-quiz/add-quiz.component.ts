import { Component, OnInit, inject } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormControl,
  FormArray,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { Router } from '@angular/router';
import { QuizService } from '../../../shared/services/quiz.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { NgFor, NgIf } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../../confirm-dialog/confirm-dialog.component';
import { AlertDialogComponent } from '../../../shared/components/alert-dialog/alert-dialog.component';

@Component({
  selector: 'app-add-quiz',
  standalone: true,
  imports: [
    NgIf,
    NgFor,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
  ],
  templateUrl: './add-quiz.component.html',
  styleUrls: ['./add-quiz.component.css'],
})
export class AddQuizComponent implements OnInit {
  form!: FormGroup;
  quizService = inject(QuizService);
  router = inject(Router);
  fb = inject(FormBuilder);
  dialog = inject(MatDialog);

  ngOnInit() {
    this.initializeForm();
  
    // Επαναφορά αποθηκευμένων δεδομένων (αν υπάρχουν)
    const savedData = localStorage.getItem('quizForm');
    if (savedData) {
      this.form.patchValue(JSON.parse(savedData)); // Εφαρμόζουμε τα αποθηκευμένα δεδομένα
    }
  
    // Αποθήκευση αλλαγών της φόρμας στο localStorage
    this.form.valueChanges.subscribe((value) => {
      localStorage.setItem('quizForm', JSON.stringify(value));
    });
  }
  

  initializeForm() {
    this.form = this.fb.group({
      title: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required),
      questions: this.fb.array(Array(10).fill(null).map(() => this.createQuestionGroup())),
    });
  }

  get questions(): FormArray {
    return this.form.get('questions') as FormArray;
  }

  createQuestionGroup(): FormGroup {
    return this.fb.group({
      text: new FormControl('', Validators.required),
      options: this.fb.array(
        Array(4).fill(null).map(() => new FormControl('', Validators.required))
      ),
      correctAnswer: new FormControl('', Validators.required),
      category: new FormControl('', Validators.required),
    });
  }

  getOptions(questionIndex: number): FormArray {
    return this.questions.at(questionIndex).get('options') as FormArray;
  }

  cancelAdd() {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
        data: {
            title: 'Cancel Adding Quiz',
            message: 'Any changes you made will not be saved. Are you sure you want to cancel?',
        },
    });

    dialogRef.afterClosed().subscribe((confirmed) => {
        if (confirmed) {
            this.router.navigate(['/admin/quizzes']); // Redirect back to quiz management page
        }
    });
}

  submitQuiz() {
    if (this.form.invalid) {
      alert('Please fill in all required fields.');
      return;
    }
  
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: 'Submit New Quiz',
        message: 'Are you sure you want to submit this quiz?',
      },
    });
  
    dialogRef.afterClosed().subscribe((confirmed) => {
      if (confirmed) {
        const quizData = this.form.value;
  
        this.quizService.addQuiz(quizData).subscribe({
          next: () => {
            localStorage.removeItem('quizForm'); // Καθαρισμός του localStorage
            const successDialog = this.dialog.open(AlertDialogComponent, {
              data: { message: 'Quiz created successfully!' },
            });
  
            // Redirect after a short delay
            setTimeout(() => {
              successDialog.close();
              this.router.navigate(['/admin/quizzes']);
            }, 900);
          },
          error: (error) => {
            console.error('Error adding quiz:', error);
            this.dialog.open(AlertDialogComponent, {
              data: { message: 'Failed to create quiz. Please try again.' },
            });
          },
        });
      }
    });
  }
}
