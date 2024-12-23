using QuizApp.Data;

namespace QuizApp.Repositories
{
    public interface IQuizRepository : IBaseRepository<Quiz>
    {
        // Εδώ μπορείς να προσθέσεις επιπλέον λειτουργίες που είναι μοναδικές για τα Quizzes
        Task<Quiz?> GetQuizWithQuestionsAsync(int quizId); // Ανάκτηση quiz μαζί με τις ερωτήσεις του
        Task<IEnumerable<Quiz>> GetQuizzesByTitleAsync(string title);
    }
}
