using Microsoft.EntityFrameworkCore;
using QuizApp.Data;

namespace QuizApp.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(QuizAppDbContext context) : base(context) { }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetUserAsync(string username, string password)
        {
            var user = await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return null;

            // Verify password using hashing
            return BCrypt.Net.BCrypt.Verify(password, user.Password) ? user : null;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var existingUser = await GetByIdAsync(user.Id);
            if (existingUser == null)
            {
                return false; // User not found
            }

            // Update fields (you can add more fields here as needed)
            existingUser.Username = user.Username ?? existingUser.Username;
            existingUser.Email = user.Email ?? existingUser.Email;

            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                // Hash the new password if it's being updated
                existingUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            }

            _dbSet.Update(existingUser);
            await SaveChangesAsync();
            return true; // Update successful
        }
    }
}
