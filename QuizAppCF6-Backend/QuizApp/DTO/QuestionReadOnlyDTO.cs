namespace QuizApp.DTO
{
    public class QuestionReadOnlyDTO
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public List<string> Options { get; set; }
        public string? Category { get; set; }
    }
}
