using QuizApp.DTO;

namespace QuizApp.Services
{
    public interface IQuizScoreService
    {
        //Task<List<UserQuizHistoryDTO>> GetUserHistoryAndHighScoresAsync(int userId);
        Task<List<UserQuizHistoryDTO>> GetUserHistoryAndHighScoresAsync(int userId, int quizId);
        Task<List<HighScoreDTO>> GetAllTimeHighScoresByQuizAsync(int quizId, int topN = 10);

    }
}
