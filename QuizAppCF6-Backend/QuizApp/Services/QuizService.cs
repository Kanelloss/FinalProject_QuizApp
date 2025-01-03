using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QuizApp.Data;
using QuizApp.DTO;
using QuizApp.Repositories;
using Serilog;
using System.Text.Json;

namespace QuizApp.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IQuizScoreRepository _quizScoreRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly ILogger<UserService> _logger;

        public QuizService(IQuizRepository quizRepository, IQuizScoreRepository quizScoreRepository, IQuestionRepository questionRepository)
        {
            _quizRepository = quizRepository;
            _quizScoreRepository = quizScoreRepository;
            _logger = new LoggerFactory().AddSerilog().CreateLogger<UserService>();
            _questionRepository = questionRepository;
        }

        // Δημιουργία Quiz
        public async Task<QuizReadOnlyDTO> CreateQuizAsync(QuizCreateDTO dto)
        {
            var quiz = new Quiz
            {
                Title = dto.Title!,
                Description = dto.Description,
                Questions = dto.Questions?.Select(q => new Question
                {
                    Text = q.Text!,
                    //Options = string.Join(",", q.Options!), // Serialize List<string> to CSV
                    Options = string.Join(",", q.Options!), // Αποθηκεύουμε τα Options ως CSV
                    CorrectAnswer = q.CorrectAnswer!,
                    Category = q.Category!
                }).ToList() ?? new List<Question>()
            };

            await _quizRepository.AddAsync(quiz);
            await _quizRepository.SaveChangesAsync();

            return new QuizReadOnlyDTO
            {
                Id = quiz.Id,
                Title = quiz.Title,
                Description = quiz.Description,
                Questions = quiz.Questions.Select(q => new QuestionReadOnlyDTO
                {
                    Id = q.Id,
                    Text = q.Text,
                    Options = q.Options.Split(',').ToList(), // Deserialize CSV to List<string>
                    Category = q.Category
                }).ToList()
            };
        }

        // Ανάκτηση Quiz με ID
        public async Task<QuizReadOnlyDTO?> GetQuizByIdAsync(int id)
        {
            var quiz = await _quizRepository.GetQuizWithQuestionsAsync(id);
            if (quiz == null) return null;

            return new QuizReadOnlyDTO
            {
                Id = quiz.Id,
                Title = quiz.Title,
                Description = quiz.Description,
                Questions = quiz.Questions.Select(q => new QuestionReadOnlyDTO
                {
                    Id = q.Id,
                    Text = q.Text,
                    Options = q.Options.Split(',').ToList(),
                    Category = q.Category
                }).ToList()
            };
        }

        // Ενημέρωση Quiz
        public async Task<bool> UpdateQuizAsync(int id, QuizUpdateDTO dto)
        {
            var quiz = await _quizRepository.GetByIdAsync(id);
            if (quiz == null) return false;

            if (!string.IsNullOrWhiteSpace(dto.Title))
            {
                quiz.Title = dto.Title!;
            }

            if (!string.IsNullOrWhiteSpace(dto.Description))
            {
                quiz.Description = dto.Description!;
            }

            var result = await _quizRepository.UpdateAsync(quiz);
            await _quizRepository.SaveChangesAsync();

            return result;
        }

        // Διαγραφή Quiz
        public async Task<bool> DeleteQuizAsync(int id)
        {
            var result = await _quizRepository.DeleteAsync(id);
            await _quizRepository.SaveChangesAsync();

            return result;
        }

        // Νέο quiz.
        public async Task<QuizStartDTO?> StartQuizAsync(int quizId)
        {
            var quiz = await _quizRepository.GetQuizWithQuestionsAsync(quizId);
            if (quiz == null) return null;

            return new QuizStartDTO
            {
                QuizId = quiz.Id,
                Title = quiz.Title,
                Questions = quiz.Questions.Select(q => new QuestionStartDTO
                {
                    Id = q.Id,
                    Text = q.Text,
                    Options = q.Options.Split(',').ToList() // Convert from SCV to List<string>
                }).ToList()
            };
        }


        public async Task<QuestionWithAnswerDTO?> GetQuestionWithAnswerAsync(int quizId, int index)
        {
            var quiz = await _quizRepository.GetQuizWithQuestionsAsync(quizId);
            if (quiz == null || index < 0 || index >= quiz.Questions.Count) return null;

            var question = quiz.Questions.ElementAt(index);   

            return new QuestionWithAnswerDTO
            {
                Id = question.Id,
                Text = question.Text,
                Options = question.Options.Split(',').ToList(),
                CorrectAnswer = question.CorrectAnswer,
                Category = question.Category
            };
        }

        public async Task<bool> UpdateQuestionAsync(int questionId, QuestionUpdateDTO dto)
        {
            // Ανάκτηση ερώτησης
            var question = await _questionRepository.GetByIdAsync(questionId);
            if (question == null)
            {
                _logger.LogWarning("Question with ID {QuestionId} not found.", questionId);
                return false; // Ερώτηση δεν βρέθηκε
            }

            // Ενημέρωση πεδίων αν υπάρχουν νέες τιμές
            question.Text = dto.Text ?? question.Text;
            question.Options = dto.Options != null ? string.Join(",", dto.Options) : question.Options; // Serialize List<string> to CSV
            question.CorrectAnswer = dto.CorrectAnswer ?? question.CorrectAnswer;
            question.Category = dto.Category ?? question.Category;

            // Αποθήκευση αλλαγών
            var result = await _questionRepository.UpdateAsync(question);
            if (result)
            {
                _logger.LogInformation("Question with ID {QuestionId} updated successfully.", questionId);
            }
            return result;
        }



        public async Task<QuizResultDTO?> EvaluateQuizAsync(int quizId, List<AnswerDTO> answers, int userId)
        {
            var quiz = await _quizRepository.GetQuizWithQuestionsAsync(quizId);
            if (quiz == null) return null;

            int score = 0;
            var questionResults = new List<QuestionResultDTO>();

            foreach (var answer in answers)
            {
                var question = quiz.Questions.ElementAtOrDefault(answer.QuestionId);
                if (question == null)
                {
                    _logger.LogError($"Question with index {answer.QuestionId} not found in quiz {quizId}.");
                    continue;
                }

                bool isCorrect = question.CorrectAnswer == answer.SelectedOption;
                if (isCorrect) score++;

                questionResults.Add(new QuestionResultDTO
                {
                    QuestionId = question.Id,
                    CorrectAnswer = question.CorrectAnswer,
                    SelectedOption = answer.SelectedOption,
                    IsCorrect = isCorrect
                });
            }

            // Δημιουργία εγγραφής QuizScore
            var quizScore = new QuizScore
            {
                UserId = userId,
                QuizId = quizId,
                Score = (int)((double)score / quiz.Questions.Count * 100), // Υπολογισμός ποσοστού
                InsertedAt = DateTime.UtcNow
            };

            await _quizScoreRepository.AddAsync(quizScore);
            await _quizScoreRepository.SaveChangesAsync();

            return new QuizResultDTO
            {
                TotalQuestions = quiz.Questions.Count,
                CorrectAnswers = score,
                Score = quizScore.Score,
                QuestionResults = questionResults
            };
        }

        public async Task<IEnumerable<QuizBasicDTO>> GetAllQuizzesAsync()
        {
            var quizzes = await _quizRepository.GetAllAsync(); // Ανάκτηση quizzes
            return quizzes.Select(quiz => new QuizBasicDTO
            {
                Id = quiz.Id,
                Title = quiz.Title,
                Description = quiz.Description
            }).ToList();
        }










        // ΔΕΝ ΚΑΝΕΙ SUBMIT, ΘΑ ΣΒΗΣΤΕΙ ΟΤΑΝ ΤΕΣΤΑΡΙΣΤΕΙ ΟΛΟ ΤΟ ΠΡΟΤΖΕΚΤ ΑΝ ΔΕΝ ΧΡΕΙΑΣΤΕΙ ΚΑΠΟΥ

        //public async Task<QuizResultDTO?> EvaluateQuizAsync(int quizId, List<AnswerDTO> answers)
        //{
        //    // Φόρτωση του Quiz μαζί με τις ερωτήσεις
        //    var quiz = await _quizRepository.GetQuizWithQuestionsAsync(quizId);
        //    if (quiz == null) return null;

        //    int score = 0;
        //    var questionResults = new List<QuestionResultDTO>();

        //    foreach (var answer in answers)
        //    {
        //        // Αναζήτηση της ερώτησης βάσει index
        //        var question = quiz.Questions.ElementAtOrDefault(answer.QuestionId); // Αν το QuestionId είναι το index

        //        // Αν η ερώτηση δεν βρεθεί
        //        if (question == null)
        //        {
        //            _logger.LogError($"Question with index {answer.QuestionId} not found in quiz {quizId}.");
        //            continue;
        //        }

        //        // Αξιολόγηση της απάντησης
        //        bool isCorrect = question.CorrectAnswer == answer.SelectedOption;
        //        if (isCorrect) score++;

        //        // Πρόσθεση αποτελέσματος στη λίστα
        //        questionResults.Add(new QuestionResultDTO
        //        {
        //            QuestionId = question.Id,
        //            CorrectAnswer = question.CorrectAnswer,
        //            SelectedOption = answer.SelectedOption,
        //            IsCorrect = isCorrect
        //        });
        //    }

        //    // Επιστροφή αποτελέσματος
        //    return new QuizResultDTO
        //    {
        //        TotalQuestions = quiz.Questions.Count,
        //        CorrectAnswers = score,
        //        Score = (int)((double)score / quiz.Questions.Count * 100),
        //        QuestionResults = questionResults
        //    };
        //}



























        // Υλοποίηση με questionId (not used).

        //public async Task<QuestionReadOnlyDTO?> GetQuestionByIdAsync(int quizId, int questionId)
        //{
        //    var quiz = await _quizRepository.GetQuizWithQuestionsAsync(quizId);
        //    if (quiz == null) return null;

        //    // Βρίσκουμε την ερώτηση με το συγκεκριμένο ID
        //    var question = quiz.Questions.FirstOrDefault(q => q.Id == questionId);
        //    if (question == null) return null;

        //    return new QuestionReadOnlyDTO
        //    {
        //        Id = question.Id,
        //        Text = question.Text,
        //        Options = question.Options.Split(',').ToList(),
        //        Category = question.Category
        //    };
        //}

        //public async Task<QuizResultDTO?> EvaluateQuizAsync(int quizId, List<AnswerDTO> answers)
        //{
        //    // Φόρτωση του Quiz μαζί με τις ερωτήσεις
        //    var quiz = await _quizRepository.GetQuizWithQuestionsAsync(quizId);
        //    if (quiz == null) return null;

        //    int score = 0;
        //    var questionResults = new List<QuestionResultDTO>();

        //    foreach (var answer in answers)
        //    {
        //        // Βρίσκουμε την ερώτηση βάσει του QuestionId
        //        var question = quiz.Questions.FirstOrDefault(q => q.Id == answer.QuestionId);
        //        if (question == null)
        //        {
        //            _logger.LogError($"Question with ID {answer.QuestionId} not found in quiz {quizId}.");
        //            continue;
        //        }

        //        // Αξιολόγηση της απάντησης
        //        bool isCorrect = question.CorrectAnswer == answer.SelectedOption;
        //        if (isCorrect) score++;

        //        // Πρόσθεση αποτελέσματος στη λίστα
        //        questionResults.Add(new QuestionResultDTO
        //        {
        //            QuestionId = question.Id,
        //            CorrectAnswer = question.CorrectAnswer,
        //            SelectedOption = answer.SelectedOption,
        //            IsCorrect = isCorrect
        //        });
        //    }

        //    // Επιστροφή αποτελέσματος
        //    return new QuizResultDTO
        //    {
        //        TotalQuestions = quiz.Questions.Count,
        //        CorrectAnswers = score,
        //        Score = (int)((double)score / quiz.Questions.Count * 100),
        //        QuestionResults = questionResults
        //    };
        //}



    }
}
