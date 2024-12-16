using Microsoft.AspNetCore.Mvc;
using QuizApp.DTO;
using QuizApp.Services;

namespace QuizApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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

            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
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

            var result = await _userService.UpdateUserAsync(id, dto);
            if (!result)
            {
                return NotFound(new { Message = $"User with ID {id} not found." });
            }

            return Ok(new { Message = "User updated successfully." });
        }

    }
}
