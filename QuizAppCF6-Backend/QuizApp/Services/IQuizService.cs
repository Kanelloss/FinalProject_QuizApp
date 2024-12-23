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
        Task<QuizResultDTO?> EvaluateQuizAsync(int quizId, List<AnswerDTO> answers); // Αξιολόγηση απαντήσεων

    }
}
