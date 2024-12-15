using System.ComponentModel.DataAnnotations;

namespace QuizApp.DTO
{
    public class QuizCreateDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string? Title { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public List<QuestionCreateDTO>? Questions { get; set; }
    }
}
