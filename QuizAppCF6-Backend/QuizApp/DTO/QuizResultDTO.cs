namespace QuizApp.DTO
{
    public class QuizResultDTO
    {
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public int Score { get; set; }
        public List<QuestionResultDTO> QuestionResults { get; set; } = new();
    }

    public class QuestionResultDTO
    {
        public int QuestionId { get; set; }
        public string CorrectAnswer { get; set; } = null!;
        public string SelectedOption { get; set; } = null!;
        public bool IsCorrect { get; set; }
    }
}
