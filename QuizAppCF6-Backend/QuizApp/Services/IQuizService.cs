using QuizApp.DTO;

namespace QuizApp.Services
{
    public interface IQuizService
    {
        Task<QuizStartDTO?> StartQuizAsync(int quizId); // Εκκίνηση Quiz
        Task<QuizReadOnlyDTO> CreateQuizAsync(QuizCreateDTO dto);
        Task<QuizReadOnlyDTO?> GetQuizByIdAsync(int id);
        Task<bool> UpdateQuizAsync(int id, QuizUpdateDTO dto);
        Task<bool> DeleteQuizAsync(int id);
        Task<QuestionReadOnlyDTO?> GetQuestionByIndexAsync(int quizId, int index); // Επιστροφή ερώτησης βάσει index
        Task<bool> UpdateQuestionAsync(int questionId, QuestionUpdateDTO dto);
        Task<QuizResultDTO?> EvaluateQuizAsync(int quizId, List<AnswerDTO> answers, int userId);
        Task<IEnumerable<QuizBasicDTO>> GetAllQuizzesAsync();


        //Task<QuestionReadOnlyDTO?> GetQuestionByIdAsync(int quizId, int questionId); // Επιστροφή ερώτησης βάσει id
        //Task<QuizResultDTO?> EvaluateQuizAsync(int quizId, List<AnswerDTO> answers); // Αξιολόγηση απαντήσεων (δεν έκανε submit).

    }
}
