using QuizApp.Core.Enums;

namespace QuizApp.DTO
{
    public class UserReadOnlyDTO
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public UserRole? UserRole { get; set; }
    }
}
