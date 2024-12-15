using QuizApp.Core.Enums;

namespace QuizApp.Data
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public UserRole UserRole { get; set; }

        public virtual ICollection<QuizScore> Scores { get; set; } = new List<QuizScore>();
    }
}
