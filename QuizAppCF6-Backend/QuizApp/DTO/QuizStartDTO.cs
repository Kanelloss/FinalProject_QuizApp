namespace QuizApp.DTO
{
    public class QuizStartDTO
    {
        public int QuizId { get; set; }
        public string Title { get; set; } = null!;
        public List<QuestionStartDTO> Questions { get; set; } = new();
    }
}


