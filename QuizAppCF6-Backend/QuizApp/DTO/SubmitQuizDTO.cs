namespace QuizApp.DTO
{
    public class SubmitQuizDTO
    {
        public List<AnswerDTO> Answers { get; set; } = new();
    }

    public class AnswerDTO
    {
        public int QuestionId { get; set; }
        public string SelectedOption { get; set; } = null!;
    }
}
