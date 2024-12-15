namespace QuizApp.Data
{
    public class Question : BaseEntity
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public string Options { get; set; } = null!;    // JSON array for options
        public string CorrectAnswer { get; set; } = null!;
        public string Category { get; set; } = null!;

        public int QuizId { get; set; }
        public virtual Quiz Quiz { get; set; } = null!;
    }
}
