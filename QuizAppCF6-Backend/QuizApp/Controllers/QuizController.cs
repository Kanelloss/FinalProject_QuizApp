using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApp.DTO;
using QuizApp.Services;
using System.Text.Json;

namespace QuizApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        // Δημιουργία Quiz
        [HttpPost]
        [Authorize(Roles = "Admin")] // Μόνο ο Admin μπορεί να δημιουργήσει quiz
        public async Task<IActionResult> CreateQuiz([FromBody] QuizCreateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid data provided.", Errors = ModelState });
            }

            var quiz = await _quizService.CreateQuizAsync(dto);
            return CreatedAtAction(
                nameof(GetQuizById),
                new { id = quiz.Id },
                new
                {
                    Message = "Quiz created successfully.",
                    Quiz = quiz
                }
            );
        }

        // Ανάκτηση Quiz με ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuizById(int id)
        {
            var quiz = await _quizService.GetQuizByIdAsync(id);
            if (quiz == null)
            {
                return NotFound(new { Message = $"Quiz with ID {id} not found." });
            }

            return Ok(quiz);
        }

        // Ενημέρωση Quiz
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] // Μόνο ο Admin μπορεί να ενημερώσει quiz
        public async Task<IActionResult> UpdateQuiz(int id, [FromBody] QuizUpdateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _quizService.UpdateQuizAsync(id, dto);
            if (!result)
            {
                return NotFound(new { Message = $"Quiz with ID {id} not found." });
            }

            return Ok(new { Message = "Quiz updated successfully." });
        }

        // Διαγραφή Quiz
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Μόνο ο Admin μπορεί να διαγράψει quiz
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            var result = await _quizService.DeleteQuizAsync(id);
            if (!result)
            {
                return NotFound(new { Message = $"Quiz with ID {id} not found." });
            }

            return Ok(new { Message = "Quiz deleted successfully." });
        }

        [HttpGet("{id}/start")]
        [Authorize] // Μόνο συνδεδεμένοι χρήστες μπορούν να ξεκινήσουν quiz
        public async Task<IActionResult> StartQuiz(int id)
        {
            var quiz = await _quizService.StartQuizAsync(id);
            if (quiz == null)
            {
                return NotFound(new { Message = $"Quiz with ID {id} not found." });
            }

            return Ok(quiz);
        }

        [HttpGet("{quizId}/questions/{index}")]
        [Authorize]
        public async Task<IActionResult> GetQuestion(int quizId, int index)
        {
            var quiz = await _quizService.GetQuizByIdAsync(quizId);
            if (quiz == null)
            {
                return NotFound(new { Message = $"Quiz with ID {quizId} not found." });
            }

            if (index < 0 || index >= quiz.Questions.Count)
            {
                return BadRequest(new { Message = "Invalid question index." });
            }

            var question = quiz.Questions.ElementAt(index);

            return Ok(new
            {
                QuestionId = question.Id,
                Text = question.Text,
                Options = question.Options
            });
        }

        [HttpPost("{quizId}/submit")]
        [Authorize]
        public async Task<IActionResult> SubmitQuiz(int quizId, [FromBody] SubmitQuizDTO dto)
        {
            var result = await _quizService.EvaluateQuizAsync(quizId, dto.Answers);
            if (result == null)
            {
                return BadRequest(new { Message = "Quiz evaluation failed. Ensure the answers are valid." });
            }

            return Ok(result);
        }
    }
}
