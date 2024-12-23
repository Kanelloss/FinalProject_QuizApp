namespace QuizApp.DTO
{
    public class UserQuizHistoryDTO
    {
        public string QuizTitle { get; set; } = null!;
        public int UserScore { get; set; }
        public DateTime PlayedAt { get; set; }
        public List<HighScoreDTO> HighScores { get; set; } = new();
    }

    public class HighScoreDTO
    {
        public string Username { get; set; } = null!;
        public int Score { get; set; }
        public DateTime AchievedAt { get; set; }
    }
}
