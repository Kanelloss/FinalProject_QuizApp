using System.ComponentModel.DataAnnotations;

namespace QuizApp.DTO
{
    public class UserSignUpDTO
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 50 characters.")]
        public string? Username { get; set; }

        [Required]
        [RegularExpression(@"(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*\W)^.{8,}$",
            ErrorMessage = "A valid password must be at least 8 characters long and include at least one uppercase letter, one lowercase letter," +
            " one number, and one special character.")]
        public string? Password { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "UserRole is required.")]
        [RegularExpression(@"^(User|Admin)$", ErrorMessage = "Role must be either 'User' or 'Admin'.")]
        public string? UserRole { get; set; }
    }
}
