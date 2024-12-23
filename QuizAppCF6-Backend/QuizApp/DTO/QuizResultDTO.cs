namespace QuizApp.DTO
{
    public class QuizResultDTO
    {
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public int Score { get; set; } // Score as a percentage
    }
}
