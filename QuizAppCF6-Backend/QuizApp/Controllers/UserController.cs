using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QuizApp.DTO;
using QuizApp.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace QuizApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IQuizScoreService _quizScoreService;

        public UserController(IUserService userService, IConfiguration configuration, IQuizScoreService quizScoreService)
        {
            _userService = userService;
            _configuration = configuration;
            _quizScoreService = quizScoreService;
        }


        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpDTO dto)
        {
            var result = await _userService.RegisterUserAsync(dto);
            if (!result)
            {
                return Conflict(new { Message = "Username is already taken." });
            }

            return Ok(new { Message = "User registered successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO dto)
        {
            var user = await _userService.AuthenticateUserAsync(dto);
            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid credentials." });
            }

            var userToken = _userService.CreateUserToken(
                user.Id,
                user.Username!,
                user.Email!,
                user.UserRole,
                _configuration["Authentication:SecretKey"]!
            );

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
                return Forbid("You are not authorized to access this resource.");
            }

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { Message = $"User with ID {id} not found." });
            }

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDTO dto)
        {
            if (dto == null)
            {
                return BadRequest(new { Message = "The dto field is required." });
            }

            try
            {
                var result = await _userService.UpdateUserAsync(id, dto);
                if (!result)
                {
                    return NotFound(new { Message = $"User with ID {id} not found." });
                }

                return Ok(new { Message = "User updated successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { Message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("protected")]
        public IActionResult TestProtectedEndpoint()
        {
            return Ok(new { Message = "You have accessed a protected endpoint!" });
        }

        [HttpGet("{userId}/history-and-highscores")]
        [Authorize]
        public async Task<IActionResult> GetHistoryAndHighScores(int userId)
        {
            try
            {
                var result = await _quizScoreService.GetUserHistoryAndHighScoresAsync(userId);
                if (result == null || !result.Any())
                {
                    return NotFound(new { Message = "No quiz history found for the user." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving quiz history.", Details = ex.Message });
            }
        }



    }
}
