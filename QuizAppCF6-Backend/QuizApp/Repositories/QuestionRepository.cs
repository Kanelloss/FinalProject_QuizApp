using QuizApp.Data;

namespace QuizApp.Repositories
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(QuizAppDbContext context) : base(context)
        {
        }
    }
}
