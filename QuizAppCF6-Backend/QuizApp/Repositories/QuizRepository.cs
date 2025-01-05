using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Repositories
{
    public class QuizRepository : BaseRepository<Quiz>, IQuizRepository
    {
        public QuizRepository(QuizAppDbContext context) : base(context)
        {
        }

        
        public async Task<Quiz?> GetQuizWithQuestionsAsync(int quizId)
        {
            return await _dbSet
                .Include(q => q.Questions)
                .FirstOrDefaultAsync(q => q.Id == quizId);
        }

        public async Task<IEnumerable<Quiz>> GetQuizzesByTitleAsync(string title)
        {
            return await _dbSet
                .Where(q => EF.Functions.Like(q.Title, $"%{title}%"))
                .Include(q => q.Questions)
                .ToListAsync();
        }
    }
}
