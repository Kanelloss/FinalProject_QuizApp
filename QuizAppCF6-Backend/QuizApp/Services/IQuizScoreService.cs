using QuizApp.DTO;

namespace QuizApp.Services
{
    public interface IQuizScoreService
    {
        Task<List<UserQuizHistoryDTO>> GetUserHistoryAndHighScoresAsync(int userId);
    }
}
