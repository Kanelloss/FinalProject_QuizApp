using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApp.DTO;
using QuizApp.Services;
using System.Security.Claims;
using System.Text.Json;

namespace QuizApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;
        private readonly IQuizScoreService _quizScoreService;

        public QuizController(IQuizService quizService, IQuizScoreService quizScoreService)
        {
            _quizService = quizService;
            _quizScoreService = quizScoreService;
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
                return BadRequest(new { Message = "The title field is required and cannot be empty." });
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

        [Authorize(Roles = "Admin")]
        [HttpPut("questions/{questionId}")]
        public async Task<IActionResult> UpdateQuestion(int questionId, [FromBody] QuestionUpdateDTO dto)
        {
            if (!User.IsInRole("Admin"))
            {
                return StatusCode(403, new { Message = "You must be an admin to update a question." });
            }

            var result = await _quizService.UpdateQuestionAsync(questionId, dto);

            if (!result)
            {
                return NotFound(new { Message = $"Question with ID {questionId} not found." });
            }

            return Ok(new { Message = "Question updated successfully." });
        }




        //[HttpGet("{quizId}/questions/{id}")]
        //[Authorize]
        //public async Task<IActionResult> GetQuestionById(int quizId, int id)
        //{
        //    var question = await _quizService.GetQuestionByIdAsync(quizId, id);
        //    if (question == null)
        //    {
        //        return NotFound(new { Message = $"Question with ID {id} not found in quiz {quizId}." });
        //    }

        //    return Ok(question);
        //}


        [HttpGet("{quizId}/questions/{index}")]
        [Authorize]
        public async Task<IActionResult> GetQuestionByIndex(int quizId, int index)
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
            // Εξαγωγή του userId από το JWT token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { Message = "User ID is invalid or missing from token." });
            }

            // Αξιολόγηση του κουίζ
            var result = await _quizService.EvaluateQuizAsync(quizId, dto.Answers, userId);
            if (result == null)
            {
                return BadRequest(new { Message = "Quiz evaluation failed. Ensure the quiz and answers are valid." });
            }

            return Ok(result);
        }

        [HttpGet("{quizId}/alltimehighscores")]
        [Authorize]
        public async Task<IActionResult> GetAllTimeHighScores(int quizId, [FromQuery] int topN = 10)
        {
            try
            {
                var highScores = await _quizScoreService.GetAllTimeHighScoresByQuizAsync(quizId, topN);
                if (highScores == null || !highScores.Any())
                {
                    return NotFound(new { Message = "No high scores found for the specified quiz." });
                }
                return Ok(highScores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving high scores.", Details = ex.Message });
            }
        }



        //[HttpPost("{quizId}/submit")]
        //[Authorize]
        //public async Task<IActionResult> SubmitQuiz(int quizId, [FromBody] SubmitQuizDTO dto)
        //{
        //    var result = await _quizService.EvaluateQuizAsync(quizId, dto.Answers);
        //    if (result == null)
        //    {
        //        return BadRequest(new { Message = "Quiz evaluation failed. Ensure the answers are valid." });
        //    }

        //    return Ok(result);
        //}
    }
}
