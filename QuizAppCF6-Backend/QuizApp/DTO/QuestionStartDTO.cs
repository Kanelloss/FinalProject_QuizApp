namespace QuizApp.DTO
{
    public class QuestionStartDTO
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public List<string> Options { get; set; } = new();
    }
}
