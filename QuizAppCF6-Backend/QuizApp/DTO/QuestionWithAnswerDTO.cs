namespace QuizApp.DTO
{
    public class QuestionWithAnswerDTO
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public List<string> Options { get; set; }
        public string? CorrectAnswer { get; set; }
        public string? Category { get; set; }
    }
}
