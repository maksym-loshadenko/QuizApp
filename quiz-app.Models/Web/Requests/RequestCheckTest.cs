using quiz_app.Models.Web.Requests.Models.Tests;

namespace quiz_app.Models.Web.Requests
{
    public class RequestCheckTest
    {
        public string TestId { get; set; } = "";
        public List<QuestionAnswer> Answers { get; set; } = new();
    }
}
