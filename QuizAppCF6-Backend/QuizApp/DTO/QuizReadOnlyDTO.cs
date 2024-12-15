namespace QuizApp.DTO
{
    public class QuizReadOnlyDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<QuestionReadOnlyDTO>? Questions { get; set; }
    }
}
