using QuizApp.Data;
using QuizApp.DTO;

namespace QuizApp.Services
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(UserSignUpDTO dto); // Register a new user
        Task<UserReadOnlyDTO?> AuthenticateUserAsync(UserLoginDTO dto); // Authenticate user credentials
        Task<UserReadOnlyDTO?> GetUserByIdAsync(int id); // Get user by ID
        Task<bool> UpdateUserAsync(int userId, UserUpdateDTO dto); // Update user details


    }
}
