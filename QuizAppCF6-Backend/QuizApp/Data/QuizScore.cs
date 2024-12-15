namespace QuizApp.Data
{
    public class QuizScore : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuizId { get; set; }
        public int Score { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Quiz Quiz { get; set; } = null!;
    }
}
