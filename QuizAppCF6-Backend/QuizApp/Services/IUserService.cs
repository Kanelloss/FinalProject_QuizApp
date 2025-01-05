using QuizApp.Core.Enums;
using QuizApp.Data;
using QuizApp.DTO;

namespace QuizApp.Services
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(UserSignUpDTO dto);                                                                    // Register a new user
        Task<UserReadOnlyDTO?> AuthenticateUserAsync(UserLoginDTO dto);                                                     // Authenticate user credentials
        Task<UserReadOnlyDTO?> GetUserByIdAsync(int id);                                                                    // Get user by id
        Task<UserReadOnlyDTO?> GetUserByUsernameAsync(string username);                                                     // Get a user by username
        Task<IEnumerable<UserReadOnlyDTO>> GetAllUsersAsync();                                                              // Get all users
        Task<bool> UpdateUserAsync(int userId, UserUpdateDTO dto);                                                          // Update user details by id
        Task<bool> DeleteUserAsync(int userId, int currentUserId, string currentUserRole);                                  // Delete user by id
        string CreateUserToken(int userId, string username, string email, UserRole? userRole, string appSecurityKey);       // Create a user token


    }
}
