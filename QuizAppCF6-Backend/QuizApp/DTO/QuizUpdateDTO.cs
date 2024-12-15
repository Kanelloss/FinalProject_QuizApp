using System.ComponentModel.DataAnnotations;

namespace QuizApp.DTO
{
    public class QuizUpdateDTO
    {
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 100 characters.")]
        public string? Title { get; set; }

        [StringLength(500, ErrorMessage = "Description must not exceed 500 characters.")]
        public string? Description { get; set; }
    }
}
