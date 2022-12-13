using quiz_app.Models.Web.Responses.Models.Tests;

namespace quiz_app.Models.Database.Tests
{
    public class Question: QuestionModel
    {
        public CorrectAnswer CorrectAnswer { get; set; } = CorrectAnswer.First;
    }
}
