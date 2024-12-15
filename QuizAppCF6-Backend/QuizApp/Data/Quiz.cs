namespace QuizApp.Data
{
    public class Quiz : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<QuizScore> QuizScores { get; set; } = new List<QuizScore>();
    }
}
