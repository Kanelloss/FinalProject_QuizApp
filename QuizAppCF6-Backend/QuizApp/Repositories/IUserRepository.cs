using QuizApp.Data;

namespace QuizApp.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetUserByUsernameAsync(string username); // Find user by username
        Task<User?> GetUserAsync(string username, string password); // Validate user by username and password
        Task<bool> UpdateUserAsync(User user); // Update user details
    }
}
