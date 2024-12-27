export interface Question {
    id: number;
    text: string;
    options: string[]; // List of possible answers
    correctAnswer: string; // The correct option
    category: string;
    quizId: number; // Foreign key reference to the Quiz
  }
  