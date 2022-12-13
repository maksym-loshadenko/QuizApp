using quiz_app.Models.Database.Tests;

namespace quiz_app.Models.Web.Requests.Models.Tests
{
    public class QuestionAnswer
    {
        public string Text { get; set; } = "";

        public CorrectAnswer Answer { get; set; }
    }
}
