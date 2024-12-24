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

        //public async Task<List<UserQuizHistoryDTO>> GetUserHistoryAndHighScoresAsync(int userId)
        //{
        //    var userScores = await _quizScoreRepository.GetScoresByUserAsync(userId);

        //    var result = userScores
        //        .GroupBy(qs => qs.QuizId)
        //        .Select(group => new UserQuizHistoryDTO
        //        {
        //            QuizTitle = group.First().Quiz.Title,
        //            UserScore = group.First(qs => qs.UserId == userId).Score,
        //            PlayedAt = group.First(qs => qs.UserId == userId).InsertedAt,
        //            HighScores = group
        //                .OrderByDescending(qs => qs.Score)
        //                .Take(3) // Top 3 scores
        //                .Select(qs => new HighScoreDTO
        //                {
        //                    Username = qs.User.Username,
        //                    Score = qs.Score,
        //                    AchievedAt = qs.InsertedAt
        //                })
        //                .ToList()
        //        }).ToList();

        //    return result;
        //}

        public async Task<List<UserQuizHistoryDTO>> GetUserHistoryAndHighScoresAsync(int userId, int quizId)
        {
            var userScores = await _quizScoreRepository.GetScoresByUserAsync(userId);

            // Φιλτράρισμα για το συγκεκριμένο quizId
            var quizScores = userScores.Where(qs => qs.QuizId == quizId).ToList();

            if (!quizScores.Any()) return new List<UserQuizHistoryDTO>();

            // Ομαδοποίηση και υπολογισμός
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

        public async Task<List<HighScoreDTO>> GetAllTimeHighScoresByQuizAsync(int quizId, int topN = 10)
        {
            // Κλήση της repository μεθόδου
            var highScores = await _quizScoreRepository.GetTopScoresByQuizAsync(quizId, topN);

            // Μετατροπή σε DTO
            return highScores.Select(qs => new HighScoreDTO
            {
                Username = qs.User.Username, // Username του χρήστη
                Score = qs.Score,            // Το σκορ του
                AchievedAt = qs.InsertedAt   // Ημερομηνία επίτευξης
            }).ToList();
        }
    }
}
