using quiz_app.Models.Web.Responses.Models.Tests;

namespace quiz_app.Models.Database.Tests
{
    public class Test
    {
        public string Id { get; set; } = "";

        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        public List<Question> Questions { get; set; } = new();

        public static explicit operator ShortTestModel(Test obj)
        {
            return new ShortTestModel()
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description
            };
        }

        public static explicit operator TestModel(Test obj)
        {
            return new TestModel()
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description,
                Questions = obj.Questions.Cast<QuestionModel>().ToList()
            };
        }
    }
}
