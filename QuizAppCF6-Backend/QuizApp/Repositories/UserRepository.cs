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

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
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

            // Update fields
            existingUser.Username = user.Username ?? existingUser.Username;
            existingUser.Email = user.Email ?? existingUser.Email;

            if (!string.IsNullOrWhiteSpace(user.Password) && user.Password != existingUser.Password)
            {
                existingUser.Password = user.Password; // Already hashed in the service
            }

            _dbSet.Update(existingUser);
            await SaveChangesAsync();
            return true;
        }





    }
}
