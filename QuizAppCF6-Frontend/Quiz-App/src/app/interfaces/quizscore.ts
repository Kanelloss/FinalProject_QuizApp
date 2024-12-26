export interface QuizScore {
    id: number;
    userId: number; // Foreign key reference to the User
    quizId: number; // Foreign key reference to the Quiz
    score: number; // Percentage score
    insertedAt: string; // Date string in ISO format
  }
  