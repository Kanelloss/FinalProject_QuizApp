using QuizApp.Data;
using QuizApp.DTO;
using QuizApp.Repositories;
using System.Text.Json;

namespace QuizApp.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;

        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
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

        public async Task<QuestionReadOnlyDTO?> GetQuestionByIndexAsync(int quizId, int index)
        {
            var quiz = await _quizRepository.GetQuizWithQuestionsAsync(quizId);
            if (quiz == null || index < 0 || index >= quiz.Questions.Count) return null;

            var question = quiz.Questions.ElementAt(index);

            return new QuestionReadOnlyDTO
            {
                Id = question.Id,
                Text = question.Text,
                Options = JsonSerializer.Deserialize<List<string>>(question.Options),
                Category = question.Category
            };
        }

        public async Task<QuizResultDTO?> EvaluateQuizAsync(int quizId, List<AnswerDTO> answers)
        {
            var quiz = await _quizRepository.GetQuizWithQuestionsAsync(quizId);
            if (quiz == null) return null;

            int score = 0;
            foreach (var answer in answers)
            {
                var question = quiz.Questions.FirstOrDefault(q => q.Id == answer.QuestionId);
                if (question != null && question.CorrectAnswer == answer.SelectedOption)
                {
                    score++;
                }
            }

            return new QuizResultDTO
            {
                TotalQuestions = quiz.Questions.Count,
                CorrectAnswers = score,
                Score = (int)((double)score / quiz.Questions.Count * 100)
            };
        }



    }
}
