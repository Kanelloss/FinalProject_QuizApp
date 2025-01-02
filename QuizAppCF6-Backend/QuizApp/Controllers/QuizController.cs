using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizApp.DTO;
using QuizApp.Services;
using Serilog;
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
        private readonly ILogger<QuizController> _logger;

        public QuizController(IQuizService quizService, IQuizScoreService quizScoreService)
        {
            _quizService = quizService;
            _quizScoreService = quizScoreService;
            _logger = new LoggerFactory().AddSerilog().CreateLogger<QuizController>();
        }

        /// <summary>
        /// Retrieves a quiz by its ID.
        /// </summary>
        /// <remarks>
        /// This endpoint allows a user to fetch a quiz's details by providing its unique ID.
        /// </remarks>
        /// <param name="id">The unique identifier of the quiz.</param>
        /// <response code="200">Returns the details of the quiz.</response>
        /// <response code="404">If the quiz is not found.</response>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetQuizById(int id)
        {
            var quiz = await _quizService.GetQuizByIdAsync(id);
            if (quiz == null)
            {
                _logger.LogWarning("Quiz with ID {QuizId} not found.", id);
                return NotFound(new { Message = $"Quiz with ID {id} not found." });
            }
            _logger.LogInformation("Quiz with ID {QuizId} retrieved successfully.", id);
            return Ok(quiz);
        }

        /// <summary>
        /// Creates a new quiz in the system.
        /// </summary>
        /// <remarks>
        /// This endpoint allows administrators to create a new quiz with questions.
        /// </remarks>
        /// <param name="dto">The details of the quiz to create.</param>
        /// <returns>
        /// Returns the created quiz and a 201 response if successful.
        /// Returns 400 for invalid data or 500 for unexpected errors.
        /// </returns>
        /// <response code="201">Quiz created successfully.</response>
        /// <response code="400">Invalid data provided.</response>
        /// <response code="500">An unexpected error occurred.</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateQuiz([FromBody] QuizCreateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("CreateQuiz failed: Invalid data provided.");
                return BadRequest(new { Message = "Invalid data provided.", Errors = ModelState });
            }

            try
            {
                var quiz = await _quizService.CreateQuizAsync(dto);
                _logger.LogInformation("Quiz with ID {QuizId} created successfully by User ID {UserId}.", quiz.Id, User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while creating quiz.");
                return StatusCode(500, new { Message = "An unexpected error occurred." });
            }
        }




        /// <summary>
        /// Modifies the details of an existing quiz.
        /// </summary>
        /// <remarks>
        /// This endpoint allows an admin to update the details of an existing quiz.
        /// The request must include valid quiz data in JSON format.
        /// </remarks>
        /// <param name="id">The unique identifier of the quiz to be updated.</param>
        /// <param name="dto">The updated quiz details.</param>
        /// <response code="200">If the quiz was updated successfully.</response>
        /// <response code="400">If the provided data is invalid.</response>
        /// <response code="404">If the quiz with the specified ID was not found.</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] // Μόνο ο Admin μπορεί να ενημερώσει quiz
        public async Task<IActionResult> UpdateQuiz(int id, [FromBody] QuizUpdateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Quiz update failed: invalid data provided for quiz ID {QuizId}.", id);
                return BadRequest(new { Message = "The title field is required and cannot be empty." });
            }

            var result = await _quizService.UpdateQuizAsync(id, dto);
            if (!result)
            {
                _logger.LogWarning("Quiz update failed: quiz with ID {QuizId} not found.", id);
                return NotFound(new { Message = $"Quiz with ID {id} not found." });
            }
            _logger.LogInformation("Quiz with ID {QuizId} updated successfully.", id);
            return Ok(new { Message = "Quiz updated successfully." });
        }

        /// <summary>
        /// Deletes a quiz by its ID.
        /// </summary>
        /// <remarks>
        /// This endpoint allows an Admin to delete an existing quiz.  
        /// Only Admin users are authorized to perform this action.
        /// </remarks>
        /// <param name="id">The ID of the quiz to delete.</param>
        /// <returns>
        /// <response code="200">The quiz was successfully deleted.</response>
        /// <response code="404">The quiz with the specified ID was not found.</response>
        /// <response code="403">The user does not have permission to delete the quiz.</response>
        /// <response code="500">An unexpected error occurred while processing the request.</response>
        /// </returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Only admin can delete a quiz
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            // Find admin who deleted quiz
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            try
            {
                var result = await _quizService.DeleteQuizAsync(id);

                if (!result)
                {
                    _logger.LogWarning("Delete failed: Quiz with ID {QuizId} not found. Attempted by Admin ID {AdminId}.", id, currentUserId);
                    return NotFound(new { Message = $"Quiz with ID {id} not found." });
                }

                _logger.LogInformation("Quiz with ID {QuizId} successfully deleted by Admin ID {AdminId}.", id, currentUserId);
                return Ok(new { Message = "Quiz deleted successfully." });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while deleting Quiz ID {QuizId} by Admin ID {AdminId}.", id, currentUserId);
                return StatusCode(StatusCodes.Status403Forbidden, new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while deleting Quiz ID {QuizId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred." });
            }
        }

        /// <summary>
        /// Starts a quiz for a user.
        /// </summary>
        /// <remarks>
        /// This endpoint initializes a quiz for a user. The user must be authenticated to access this endpoint.
        /// </remarks>
        /// <param name="id">The ID of the quiz to start.</param>
        /// <response code="200">Quiz started successfully.</response>
        /// <response code="401">Unauthorized. The user must log in to access this resource.</response>
        /// <response code="404">Quiz not found.</response>
        /// <response code="500">An unexpected error occurred.</response>
        [HttpGet("{id}/start")]
        [Authorize]
        public async Task<IActionResult> StartQuiz(int id)
        {
            // Retrieve user information from JWT
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            try
            {
                var quiz = await _quizService.StartQuizAsync(id);
                if (quiz == null)
                {
                    _logger.LogWarning("Quiz with ID {QuizId} not found. Requested by User ID {UserId}.", id, currentUserId);
                    return NotFound(new { Message = $"Quiz with ID {id} not found." });
                }

                _logger.LogInformation("Quiz with ID {QuizId} successfully started by User ID {UserId}.", id, currentUserId);
                return Ok(new { Message = "Quiz started successfully.", Quiz = quiz });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while starting quiz ID {QuizId} by User ID {UserId}.", id, currentUserId);
                return StatusCode(500, new { Message = "An unexpected error occurred while starting the quiz." });
            }
        }

        /// <summary>
        /// Updates a specific question in a quiz.
        /// </summary>
        /// <remarks>
        /// This endpoint allows an admin to update the details of a question, including its text, options, correct answer, and category.
        /// </remarks>
        /// <param name="questionId">The ID of the question to update.</param>
        /// <param name="dto">The updated details of the question.</param>
        /// <response code="200">Question updated successfully.</response>
        /// <response code="400">Bad Request. The request body is invalid or empty.</response>
        /// <response code="401">Unauthorized. The user must log in to access this resource.</response>
        /// <response code="403">Forbidden. Only admins can access this resource.</response>
        /// <response code="404">Question not found.</response>
        /// <response code="500">An unexpected error occurred.</response>
        [Authorize(Roles = "Admin")]
        [HttpPut("questions/{questionId}")]
        public async Task<IActionResult> UpdateQuestion(int questionId, [FromBody] QuestionUpdateDTO dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Update failed: No data provided for question ID {QuestionId}.", questionId);
                return BadRequest(new { Message = "The request body cannot be empty." });
            }

            // Retrieve current admin user information
            var currentAdminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            try
            {
                var result = await _quizService.UpdateQuestionAsync(questionId, dto);

                if (!result)
                {
                    _logger.LogWarning("Update failed: Question with ID {QuestionId} not found. Attempted by Admin ID {AdminId}.", questionId, currentAdminId);
                    return NotFound(new { Message = $"Question with ID {questionId} not found." });
                }

                _logger.LogInformation("Question with ID {QuestionId} successfully updated by Admin ID {AdminId}.", questionId, currentAdminId);
                return Ok(new { Message = "Question updated successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while updating question ID {QuestionId} by Admin ID {AdminId}.", questionId, currentAdminId);
                return StatusCode(500, new { Message = "An unexpected error occurred while updating the question." });
            }
        }

        /// <summary>
        /// Retrieves a specific question by its index from a quiz.
        /// </summary>
        /// <remarks>
        /// This endpoint allows authenticated users to fetch a specific question by providing the quiz ID and the question's index.
        /// </remarks>
        /// <param name="quizId">The ID of the quiz.</param>
        /// <param name="index">The index of the question within the quiz.</param>
        /// <response code="200">Returns the question details (ID, text, options).</response>
        /// <response code="400">Invalid question index provided.</response>
        /// <response code="404">Quiz with the specified ID not found.</response>
        /// <response code="401">Unauthorized. The user must be authenticated to access this endpoint.</response>
        [HttpGet("{quizId}/questions/{index}")]
        [Authorize]
        public async Task<IActionResult> GetQuestionByIndex(int quizId, int index)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                _logger.LogInformation("User with ID {UserId} requested question {Index} from quiz {QuizId}.", userId, index, quizId);

                var quiz = await _quizService.GetQuizByIdAsync(quizId);
                if (quiz == null)
                {
                    _logger.LogWarning("Quiz with ID {QuizId} not found for User ID {UserId}.", quizId, userId);
                    return NotFound(new { Message = $"Quiz with ID {quizId} not found." });
                }

                if (index < 0 || index >= quiz.Questions!.Count)
                {
                    _logger.LogWarning("Invalid question index {Index} requested by User ID {UserId} for Quiz ID {QuizId}.", index, userId, quizId);
                    return BadRequest(new { Message = "Invalid question index." });
                }

                var question = quiz.Questions.ElementAt(index);

                _logger.LogInformation("Question {Index} from Quiz ID {QuizId} returned successfully to User ID {UserId}.", index, quizId, userId);

                return Ok(new
                {
                    QuestionId = question.Id,
                    Text = question.Text,
                    Options = question.Options
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving question {Index} from quiz {QuizId} by User ID {UserId}.", index, quizId, userId);
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }


        /// <summary>
        /// Submits the user's answers for a specific quiz and evaluates the score.
        /// </summary>
        /// <remarks>
        /// This endpoint allows the user to submit their answers for a quiz. The answers must be indexed starting from `questionId: 0` and follow the sequence of questions as retrieved from the quiz.
        /// 
        /// **Example Request Body:**
        /// ```json
        /// {
        ///   "answers": [
        ///     { "questionId": 0, "selectedOption": "H2O1" },
        ///     { "questionId": 1, "selectedOption": "100°C" },
        ///     { "questionId": 2, "selectedOption": "Carbon Dioxide" },
        ///     { "questionId": 3, "selectedOption": "Au" }
        ///   ]
        /// }
        /// ```
        /// </remarks>
        /// <param name="quizId">The ID of the quiz being submitted.</param>
        /// <param name="dto">The user's answers in JSON format. Each answer must include a `questionId` starting from 0 and a `selectedOption`.</param>
        /// <returns>
        /// **200 OK:** Returns the evaluation results, including the score and the correctness of each answer.
        /// **400 Bad Request:** Indicates invalid input or missing required fields.
        /// **404 Not Found:** Indicates that the quiz with the specified ID was not found.
        /// **500 Internal Server Error:** Indicates an unexpected error during processing.
        /// </returns>
        [HttpPost("{quizId}/submit")]
        [Authorize]
        public async Task<IActionResult> SubmitQuiz(int quizId, [FromBody] SubmitQuizDTO dto)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(currentUserId) || !int.TryParse(currentUserId, out int userId))
            {
                _logger.LogWarning("Unauthorized quiz submission attempt. Missing or invalid user ID.");
                return Unauthorized(new { Message = "Invalid or missing user ID in the token." });
            }

            if (dto == null || dto.Answers == null || !dto.Answers.Any())
            {
                _logger.LogWarning("Quiz submission failed for User ID {UserId}: Missing or invalid answers.", userId);
                return BadRequest(new { Message = "Answers are required for quiz submission." });
            }

            try
            {
                var result = await _quizService.EvaluateQuizAsync(quizId, dto.Answers, userId);
                if (result == null)
                {
                    _logger.LogWarning("Quiz submission failed for User ID {UserId} on Quiz ID {QuizId}. Invalid quiz or answers.", userId, quizId);
                    return BadRequest(new { Message = "Quiz evaluation failed. Ensure the quiz ID and answers are valid." });
                }

                _logger.LogInformation("Quiz ID {QuizId} successfully submitted by User ID {UserId}. Score: {Score}.", quizId, userId, result.Score);
                return Ok(new
                {
                    Message = "Quiz submitted successfully.",
                    Result = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during quiz submission for Quiz ID {QuizId} by User ID {UserId}.", quizId, userId);
                return StatusCode(500, new { Message = "An unexpected error occurred while submitting the quiz." });
            }
        }

        /// <summary>
        /// Retrieves all-time high scores for a specific quiz.
        /// </summary>
        /// <remarks>
        /// This endpoint returns the top N all-time high scores for a specific quiz.
        /// It also includes the title of the quiz for display purposes.
        /// </remarks>
        /// <param name="quizId">The unique ID of the quiz for which high scores are requested.</param>
        /// <param name="topN">The number of top scores to retrieve (default is 10).</param>
        /// <response code="200">Returns the quiz title and the top N high scores.</response>
        /// <response code="404">If the quiz or high scores are not found.</response>
        /// <response code="500">If an unexpected error occurs.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [HttpGet("{quizId}/alltimehighscores")]
        public async Task<IActionResult> GetAllTimeHighScores(int quizId, [FromQuery] int topN = 10)
        {
            // Διατήρηση του currentUserId για logging ή άλλες χρήσεις
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            try
            {
                // Ανάκτηση quiz details
                var quiz = await _quizService.GetQuizByIdAsync(quizId);
                if (quiz == null)
                {
                    _logger.LogWarning("Quiz with ID {QuizId} not found. Requested by User ID {UserId}.", quizId, currentUserId);
                    return NotFound(new { Message = $"Quiz with ID {quizId} not found." });
                }

                // Ανάκτηση σκορ
                var highScores = await _quizScoreService.GetAllTimeHighScoresByQuizAsync(quizId, topN);
                if (highScores == null || !highScores.Any())
                {
                    _logger.LogWarning("No high scores found for quiz ID {QuizId}. Requested by User ID {UserId}.", quizId, currentUserId);
                    return NotFound(new { Message = "No high scores found for the specified quiz." });
                }

                _logger.LogInformation("High scores retrieved successfully for quiz ID {QuizId} by User ID {UserId}.", quizId, currentUserId);

                // Επιστροφή quiz title και high scores
                return Ok(new
                {
                    QuizTitle = quiz.Title, // Τίτλος του quiz
                    HighScores = highScores // Σκορ
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving high scores for quiz ID {QuizId} requested by User ID {UserId}.", quizId, currentUserId);
                return StatusCode(500, new { Message = "An error occurred while retrieving high scores.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves all quizzes in the system.
        /// </summary>
        /// <remarks>
        /// This endpoint allows an admin to retrieve a list of all quizzes, including their ID, title, and description.
        /// </remarks>
        /// <returns>
        /// A list of quizzes in JSON format.
        /// </returns>
        /// <response code="200">Returns the list of quizzes.</response>
        /// <response code="404">No quizzes were found.</response>
        /// <response code="401">Unauthorized. The user must be logged in.</response>
        /// <response code="403">Forbidden. Only admins can access this endpoint.</response>
        /// <response code="500">An unexpected error occurred.</response>
        [HttpGet("getall")]
        [Authorize(Roles = "Admin")] // Μόνο ο Admin μπορεί να δει όλα τα quizzes
        public async Task<IActionResult> GetAllQuizzes()
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            try
            {
                var quizzes = await _quizService.GetAllQuizzesAsync();
                if (quizzes == null || !quizzes.Any())
                {
                    _logger.LogWarning("No quizzes found. Requested by Admin ID {AdminId}.", currentUserId);
                    return NotFound(new { Message = "No quizzes found." });
                }
                _logger.LogInformation("All quizzes retrieved successfully by Admin ID {AdminId}.", currentUserId);
                return Ok(quizzes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all quizzes.");
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
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
