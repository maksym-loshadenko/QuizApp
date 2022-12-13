namespace quiz_app.Models.Web.Responses.Models.Tests
{
    public class TestModel: ShortTestModel
    {
        public List<QuestionModel> Questions { get; set; } = new();
    }
}
