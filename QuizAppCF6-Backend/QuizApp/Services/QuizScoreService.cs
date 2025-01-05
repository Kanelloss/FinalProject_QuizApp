using QuizApp.DTO;
using QuizApp.Repositories;

namespace QuizApp.Services
{
    public class QuizScoreService : IQuizScoreService
    {
        private readonly IQuizScoreRepository _quizScoreRepository;

        public QuizScoreService(IQuizScoreRepository quizScoreRepository)
        {
            _quizScoreRepository = quizScoreRepository;
        }
        //  Get a user's history and highscores
        public async Task<List<UserQuizHistoryDTO>> GetUserHistoryAndHighScoresAsync(int userId, int quizId)
        {
            var userScores = await _quizScoreRepository.GetScoresByUserAsync(userId);

            // Filter for specific quizId
            var quizScores = userScores.Where(qs => qs.QuizId == quizId).ToList();

            if (!quizScores.Any()) return new List<UserQuizHistoryDTO>();

            var result = quizScores
                .GroupBy(qs => qs.QuizId)
                .Select(group => new UserQuizHistoryDTO
                {
                    QuizTitle = group.First().Quiz.Title,
                    UserScore = group.First(qs => qs.UserId == userId).Score,
                    PlayedAt = group.First(qs => qs.UserId == userId).InsertedAt,
                    HighScores = group
                        .OrderByDescending(qs => qs.Score)
                        .Take(3) // Top 3 scores
                        .Select(qs => new HighScoreDTO
                        {
                            Username = qs.User.Username,
                            Score = qs.Score,
                            AchievedAt = qs.InsertedAt
                        })
                        .ToList()
                }).ToList();

            return result;
        }

        // Get top (n) all time highscores for a specific quiz by id
        public async Task<List<HighScoreDTO>> GetAllTimeHighScoresByQuizAsync(int quizId, int topN = 10)
        {
            var highScores = await _quizScoreRepository.GetTopScoresByQuizAsync(quizId, topN);

            // Turn into a DTO
            return highScores.Select(qs => new HighScoreDTO
            {
                Username = qs.User.Username,
                Score = qs.Score,            
                AchievedAt = qs.InsertedAt  // Achieved date
            }).ToList();

            
        }
    }
}
