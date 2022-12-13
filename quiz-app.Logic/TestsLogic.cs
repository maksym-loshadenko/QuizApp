using quiz_app.DbMock;
using quiz_app.Models.Database.Tests;
using quiz_app.Models.Web.Requests;
using quiz_app.Models.Web.Requests.Models.Tests;
using quiz_app.Models.Web.Responses.Models.Tests;

namespace quiz_app.Logic
{
    public class TestsLogic
    {
        public static int GetTestScore(RequestCheckTest test)
        {
            double scorePerQuestion = 100.0f / test.Answers.Count;

            var score = test.Answers.Where(answer => IsAnswerCorrect(test.TestId, answer)).Sum(answer => scorePerQuestion);

            return Convert.ToInt32(Math.Round(score));
        }

        private static bool IsAnswerCorrect(string testId, QuestionAnswer answer)
        {
            var foundTest = GetDatabaseTestById(testId);

            var question = foundTest.Questions.FirstOrDefault(q => q.Text == answer.Text);

            if (question == null)
                throw new Exception("Unable to check test. Invalid answers signature.");

            return question.CorrectAnswer == answer.Answer;
        }

        public static List<ShortTestModel> GetAvailableTests(string username)
        {
            var result = new List<ShortTestModel>();
            var testsList = TestsMock.GetList();
            var availableTestsIds = UserTestsAccessMock.GetList().Where(t => t.Username == username)
                .Select(t => t.TestId).ToList();

            var availableTests =
                availableTestsIds.Select(testId => testsList.FirstOrDefault(t => t.Id == testId)).ToList();

            foreach (var test in availableTests)
            {
                if (test != null) result.Add((ShortTestModel)test);
            }

            return result;
        }

        public static TestModel GetTestById(string id)
        {
            return (TestModel)GetDatabaseTestById(id);
        }

        public static bool DoesHaveAccessToTest(string username, string id)
        {
            if (TestsMock.GetList().All(t => t.Id != id))
                throw new Exception("Test with provided id not found.");

            return UserTestsAccessMock.GetList().Any(d => d.Username == username && d.TestId == id);
        }

        private static Test GetDatabaseTestById(string id)
        {
            var testsList = DbMock.TestsMock.GetList();
            var foundTest = testsList.FirstOrDefault(t => t.Id == id);

            if (foundTest == null)
                throw new Exception("Test with provided id not found.");

            return foundTest;
        }
    }
}
