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

        /// <summary>
        /// Registers a new user into the system.
        /// </summary>
        /// <remarks>
        /// This endpoint is used to create a new user account. 
        /// The request must include the user's username, password, email, and role (user/admin).
        /// </remarks>
        /// <param name="dto">The user registration details in JSON format.</param>
        /// <response code="200">Indicates successful registration.</response>
        /// <response code="400">Invalid input provided.</response>
        /// <response code="409">The username or email is already taken.</response>
        /// <response code="500">An unexpected error occurred.</response> 
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



        /// <summary>
        /// Authenticates a user and generates a JWT token.
        /// </summary>
        /// <remarks>
        /// This endpoint validates the provided username and password 
        /// and returns a JWT token if authentication is successful.
        /// </remarks>
        /// <param name="dto">The user's login credentials.</param>
        /// <response code="200">Login successful, JWT token returned.</response>
        /// <response code="401">Invalid username or password.</response>
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


        /// <summary>
        /// Retrieves a user's details by their ID.
        /// </summary>
        /// <remarks>
        /// This endpoint returns a user's details, including their username, email, and role, by their unique ID.
        /// </remarks>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <response code="200">User details retrieved successfully.</response>
        /// <response code="404">User not found.</response>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)    // Admin can see every user's info, User can see only his own.
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


        /// <summary>
        /// Retrieves a user's details by their username.
        /// </summary>
        /// <remarks>
        /// This endpoint returns a user's details, including their email and role, by their unique username.
        /// </remarks>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <response code="200">User details retrieved successfully.</response>
        /// <response code="404">User not found.</response>
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

        /// <summary>
        /// Retrieves all users from the system.
        /// </summary>
        /// <remarks>
        /// This endpoint is available to administrators and returns a list of all registered users, including their basic details.
        /// </remarks>
        /// <response code="200">A list of users retrieved successfully.</response>
        /// <response code="404">No users found in the system.</response>
        /// <response code="500">An unexpected error occurred.</response>
        [HttpGet("getall")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            // Retrieve user information from JWT
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            try
            {
                var users = await _userService.GetAllUsersAsync();
                if (users == null || !users.Any())
                {
                    _logger.LogWarning("No users found. Requested by Admin ID {AdminId}.", currentUserId);
                    return NotFound(new { Message = "No users found." });
                }

                _logger.LogInformation("All users retrieved successfully by Admin ID {AdminId}.", currentUserId);
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while retrieving users. Requested by Admin ID {AdminId}.", currentUserId);
                return StatusCode(500, new { Message = "An unexpected error occurred." });
            }
        }



        /// <summary>
        /// Updates user details.
        /// </summary>
        /// <remarks>
        /// This endpoint allows an admin to update any user's details or a user to update their own account details.
        /// </remarks>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="dto">The updated user details.</param>
        /// <returns>
        /// A 200 response indicates the user was successfully updated. 
        /// A 400 response indicates invalid input. 
        /// A 403 response indicates unauthorized access. 
        /// A 404 response indicates the user was not found. 
        /// A 409 response indicates a conflict, such as a duplicate username or email. 
        /// A 500 response indicates an unexpected error occurred.
        /// </returns>
        /// <response code="200">User successfully updated.</response>
        /// <response code="400">Invalid input.</response>
        /// <response code="404">User not found.</response>
        /// <response code="409">Conflict: duplicate username or email.</response>
        /// <response code="500">An unexpected error occurred.</response>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDTO dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Update failed: No data provided for user ID {UserId}.", id);
                return BadRequest(new { Message = "The dto field is required." });
            }

            // Retrieve current user information from the JWT token
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

            // Allow only if the user is Admin or updating their own data
            if (currentUserRole != "Admin" && currentUserId != id)
            {
                _logger.LogWarning("Unauthorized update attempt: User ID {CurrentUserId} tried to update User ID {TargetUserId}.", currentUserId, id);
                return StatusCode(StatusCodes.Status403Forbidden, new { Message = "You are not authorized to update this user." });
            }

            try
            {
                var result = await _userService.UpdateUserAsync(id, dto);
                if (!result)
                {
                    _logger.LogWarning("Update failed: User with ID {UserId} not found.", id);
                    return NotFound(new { Message = $"User with ID {id} not found." });
                }

                _logger.LogInformation("User with ID {UserId} updated successfully by User ID {CurrentUserId}.", id, currentUserId);
                return Ok(new { Message = "User updated successfully." });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Update failed for user ID {UserId} by User ID {CurrentUserId}: {Error}", id, currentUserId, ex.Message);
                return Conflict(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while updating user ID {UserId} by User ID {CurrentUserId}.", id, currentUserId);
                return StatusCode(500, new { Message = "An unexpected error occurred." });
            }
        }

        /// <summary>
        /// Deletes a user from the system.
        /// </summary>
        /// <remarks>
        /// This endpoint allows an admin to delete any user or a user to delete their own account.
        /// </remarks>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>
        /// A 200 response indicates the user was successfully deleted. 
        /// A 404 response indicates the user was not found. 
        /// A 500 response indicates an unexpected error occurred.
        /// </returns>
        /// <response code="200">User successfully deleted.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">An unexpected error occurred.</response>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

            try
            {
                var result = await _userService.DeleteUserAsync(id, currentUserId, currentUserRole);
                if (!result)
                {
                    _logger.LogWarning("Delete attempt failed: User with ID {UserId} not found. Requested by User ID {CurrentUserId}.", id, currentUserId);
                    return NotFound(new { Message = $"User with ID {id} not found." });
                }
                _logger.LogInformation("User with ID {UserId} successfully deleted by User ID {CurrentUserId}.", id, currentUserId);
                return Ok(new { Message = $"User with ID {id} successfully deleted." });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning("Unauthorized delete attempt by User ID {CurrentUserId} for User ID {UserId}.", currentUserId, id);
                return StatusCode(StatusCodes.Status403Forbidden, new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while deleting User ID: {Id} by User ID: {CurrentUserId}", id, currentUserId);
                return StatusCode(500, new { Message = "An unexpected error occurred." });
            }
        }

        /// <summary>
        /// Retrieves a user's quiz history and high scores.
        /// </summary>
        /// <remarks>
        /// Returns the user's played quizzes, their scores, and the high scores for the quizzes they've participated in.
        /// </remarks>
        /// <param name="userId">The ID of the user to retrieve history for.</param>
        /// <param name="quizId">The ID of the quiz to retrieve history for.</param>
        /// <response code="200">Quiz history and high scores retrieved successfully.</response>
        /// <response code="404">User not found or no quiz history available.</response>
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
