using Microsoft.EntityFrameworkCore;
using QuizApp.Data;

namespace QuizApp.Repositories
{
    public class QuizScoreRepository : BaseRepository<QuizScore>, IQuizScoreRepository
    {
        public QuizScoreRepository(QuizAppDbContext context) : base(context)
        {
        }

        public async Task<List<QuizScore>> GetScoresByUserAsync(int userId)
        {
            return await _dbSet
                .Where(qs => qs.UserId == userId)
                .Include(qs => qs.Quiz) // Include Quiz details
                .Include(qs => qs.User) // Include User details
                .ToListAsync();
        }

        public async Task<List<QuizScore>> GetTopScoresByQuizAsync(int quizId, int topN)
        {
            return await _dbSet
                .Where(qs => qs.QuizId == quizId)
                .OrderByDescending(qs => qs.Score)
                .Take(topN)
                .Include(qs => qs.User) // Include User details for displaying username
                .ToListAsync();
        }
    }
}
