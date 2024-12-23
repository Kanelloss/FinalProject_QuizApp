using QuizApp.Data;

namespace QuizApp.Repositories
{
    public interface IQuizScoreRepository : IBaseRepository<QuizScore>
    {
        Task<List<QuizScore>> GetScoresByUserAsync(int userId);
        Task<List<QuizScore>> GetTopScoresByQuizAsync(int quizId, int topN);
    }
}
