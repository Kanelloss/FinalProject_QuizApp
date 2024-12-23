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

        public async Task<List<UserQuizHistoryDTO>> GetUserHistoryAndHighScoresAsync(int userId)
        {
            var userScores = await _quizScoreRepository.GetScoresByUserAsync(userId);

            var result = userScores
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
    }
}
