using QuizApp.DTO;

namespace QuizApp.Services
{
    public interface IQuizService
    {
        Task<QuizStartDTO?> StartQuizAsync(int quizId);                                                   // Start a new quiz 
        Task<QuizReadOnlyDTO> CreateQuizAsync(QuizCreateDTO dto);                                         // Create a new quiz 
        Task<QuizReadOnlyDTO?> GetQuizByIdAsync(int id);                                                  // Get a quiz by id
        Task<bool> UpdateQuizAsync(int id, QuizUpdateDTO dto);                                            // Update a quiz by id
        Task<bool> DeleteQuizAsync(int id);                                                               // Delete a quiz by id
        Task<QuestionWithAnswerDTO?> GetQuestionWithAnswerAsync(int quizId, int index);                   // Return a question based on index including its answer
        Task<bool> UpdateQuestionAsync(int questionId, QuestionUpdateDTO dto);                            // Update a specific question by id
        Task<QuizResultDTO?> EvaluateQuizAsync(int quizId, List<AnswerDTO> answers, int userId);          // Compare the answers the user has given with the correct answers and give a result
        Task<IEnumerable<QuizBasicDTO>> GetAllQuizzesAsync();                                             // Get all available quizzes from the DB.
    }
}
