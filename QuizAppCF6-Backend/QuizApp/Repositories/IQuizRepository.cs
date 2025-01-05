using QuizApp.Data;

namespace QuizApp.Repositories
{
    public interface IQuizRepository : IBaseRepository<Quiz>
    {
        Task<Quiz?> GetQuizWithQuestionsAsync(int quizId);
        Task<IEnumerable<Quiz>> GetQuizzesByTitleAsync(string title);
    }
}
