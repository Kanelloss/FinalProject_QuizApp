using System.ComponentModel.DataAnnotations;

namespace QuizApp.DTO
{
    public class QuestionCreateDTO
    {
        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string? Text { get; set; }

        [Required]
        [MinLength(2)]
        public List<string>? Options { get; set; }

        [Required]
        public string? CorrectAnswer { get; set; }

        [Required]
        [StringLength(50)]
        public string? Category { get; set; }
    }
}
