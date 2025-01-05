using QuizApp.DTO;

namespace QuizApp.Services
{
    public interface IQuizScoreService
    {
        Task<List<UserQuizHistoryDTO>> GetUserHistoryAndHighScoresAsync(int userId, int quizId);    //  Get a user's history and highscores
        Task<List<HighScoreDTO>> GetAllTimeHighScoresByQuizAsync(int quizId, int topN = 10);        // Get top (n) all time highscores for a specific quiz by id

    }
}
