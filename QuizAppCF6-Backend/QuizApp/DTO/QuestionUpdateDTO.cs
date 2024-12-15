using System.ComponentModel.DataAnnotations;

namespace QuizApp.DTO
{
    public class QuestionUpdateDTO
    {
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Question text must be between 10 and 500 characters.")]
        public string? Text { get; set; }

        [MinLength(2, ErrorMessage = "There must be at least 2 options.")]
        public List<string>? Options { get; set; }

        public string? CorrectAnswer { get; set; }

        [StringLength(50, ErrorMessage = "Category must not exceed 50 characters.")]
        public string? Category { get; set; }
    }
}
