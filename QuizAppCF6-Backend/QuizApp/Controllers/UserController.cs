using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QuizApp.DTO;
using QuizApp.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Serilog;

namespace QuizApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IQuizScoreService _quizScoreService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, IConfiguration configuration, IQuizScoreService quizScoreService)
        {
            _userService = userService;
            _configuration = configuration;
            _quizScoreService = quizScoreService;
            _logger = new LoggerFactory().AddSerilog().CreateLogger<UserController>();
        }


        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpDTO dto)
        {
            try
            {
                var result = await _userService.RegisterUserAsync(dto);
                if (!result)
                {
                    _logger.LogWarning("SignUp failed: Username {Username} is already taken.", dto.Username);
                    return Conflict(new { Message = "Username is already taken." });
                }

                _logger.LogInformation("User {Username} registered successfully.", dto.Username);
                return Ok(new { Message = "User registered successfully." });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("SignUp failed for username {Username}: {ErrorMessage}", dto.Username, ex.Message);
                return Conflict(new { Message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("SignUp failed for username {Username}: {ErrorMessage}", dto.Username, ex.Message);
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during SignUp for username {Username}", dto.Username);
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO dto)
        {

            var user = await _userService.AuthenticateUserAsync(dto);
            if (user == null)
            {
                _logger.LogWarning("Failed login attempt for user: {Username}", dto.Username);
                return Unauthorized(new { Message = "Invalid credentials." });
            }

            var userToken = _userService.CreateUserToken(
                user.Id,
                user.Username!,
                user.Email!,
                user.UserRole,
                _configuration["Authentication:SecretKey"]!
            );

            _logger.LogInformation("User {Username} logged in successfully.", dto.Username);

            // Επιστροφή token μέσω DTO
            JwtTokenDTO token = new()
            {
                Token = userToken
            };

            return Ok(token);
        }

        // Admin can see every user's info, User can see only his own.
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {

            // Retrieve information from JWT token
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

            // Checks if Role is Admin or User is finding himself.
            if (currentUserRole != "Admin" && currentUserId != id)
            {
                _logger.LogWarning("Unauthorized access attempt by user {UserId} with role {Role}. Attempted to access user {TargetId}.",
            currentUserId, currentUserRole, id);
                return Unauthorized(new { Message = "You are not authorized to access this resource." });
            }

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                _logger.LogInformation("User with ID {UserId} not found.", id);
                return NotFound(new { Message = $"User with ID {id} not found." });
            }
            _logger.LogInformation("User with ID {UserId} retrieved successfully by user {CurrentUserId}.", id, currentUserId);
            return Ok(user);
        }

        [Authorize] // Basic authorization to allow authenticated users
        [HttpGet("by-username/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            // Retrieve the current user's role and information
            var currentUser = User.FindFirst(ClaimTypes.Name)?.Value;
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            // Check if the current user is an Admin
            if (userRole != "Admin")
            {
                _logger.LogWarning("Access denied: User {CurrentUser} (ID: {CurrentUserId}, Role: {UserRole}) attempted to retrieve user by username: {Username}.", currentUser, currentUserId, userRole, username);
                return StatusCode(StatusCodes.Status403Forbidden, new
                {
                    Message = "Access denied. Only administrators can perform this action.",
                    AttemptedBy = currentUser,
                    Role = userRole,
                    AttemptedAt = DateTime.UtcNow
                });
            }


            // Retrieve the user by username
            var user = await _userService.GetUserByUsernameAsync(username);
            if (user == null)
            {
                _logger.LogWarning("Admin {CurrentUser} could not find user with username {Username}.", currentUser, username);
                return NotFound(new { Message = $"User with username {username} not found." });
            }

            _logger.LogInformation("Admin {CurrentUser} successfully retrieved user {Username}.", currentUser, username);
            return Ok(user);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDTO dto)
        {

           
            if (dto == null)
            {
                _logger.LogWarning("Update failed: No data provided for user ID {UserId}.", id);
                return BadRequest(new { Message = "The dto field is required." });
            }

            try
            {
                var result = await _userService.UpdateUserAsync(id, dto);
                if (!result)
                {
                    _logger.LogWarning("Update failed: User with ID {UserId} not found.", id);
                    return NotFound(new { Message = $"User with ID {id} not found." });
                }

                _logger.LogInformation("User with ID {UserId} updated successfully.", id);
                return Ok(new { Message = "User updated successfully." });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Update failed for user ID {UserId}: {Error}", id, ex.Message);
                return Conflict(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while updating user ID {UserId}.", id);
                return StatusCode(500, new { Message = "An unexpected error occurred." });
            }
        }


        [HttpGet("{userId}/quiz/{quizId}/history-and-highscores")]
        [Authorize]
        public async Task<IActionResult> GetHistoryAndHighScores(int userId, int quizId)
        {
            // Retrieve current user information from the JWT token
            var currentUser = User.FindFirst(ClaimTypes.Name)?.Value;
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
           
            try
            {
                var result = await _quizScoreService.GetUserHistoryAndHighScoresAsync(userId, quizId);
                if (result == null || !result.Any())
                {
                    _logger.LogWarning("No high scores found for quiz ID {QuizId}. Accessed by {CurrentUser} (ID: {CurrentUserId}).", quizId, currentUser, currentUserId);
                    return NotFound(new { Message = "No quiz history found for the user in this quiz." });
                }

                _logger.LogInformation("User {CurrentUser} (ID: {CurrentUserId}) successfully retrieved high scores for quiz ID {QuizId}.", currentUser, currentUserId, quizId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving high scores for quiz ID {QuizId}. Accessed by {CurrentUser} (ID: {CurrentUserId}).", quizId, currentUser, currentUserId);
                return StatusCode(500, new { Message = "An error occurred while retrieving quiz history.", Details = ex.Message });
            }
        }


        //[Authorize]
        //[HttpGet("protected")]
        //public IActionResult TestProtectedEndpoint()
        //{
        //    return Ok(new { Message = "You have accessed a protected endpoint!" });
        //}

        //[HttpGet("{userId}/history-and-highscores")]
        //[Authorize]
        //public async Task<IActionResult> GetHistoryAndHighScores(int userId)
        //{
        //    try
        //    {
        //        var result = await _quizScoreService.GetUserHistoryAndHighScoresAsync(userId);
        //        if (result == null || !result.Any())
        //        {
        //            return NotFound(new { Message = "No quiz history found for the user." });
        //        }
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Message = "An error occurred while retrieving quiz history.", Details = ex.Message });
        //    }
        //}



    }
}
