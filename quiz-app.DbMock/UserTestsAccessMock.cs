using quiz_app.Models.Database.Tests;

namespace quiz_app.DbMock
{
    public class UserTestsAccessMock
    {
        public static List<UserTestAccess> GetList()
        {
            return new List<UserTestAccess>()
            {
                new()
                {
                    TestId = "81536f98-c504-4c13-93ff-4f6bd087858c",
                    Username = "user1",
                },
                new()
                {
                    TestId = "c11bdaa9-c850-4fb5-87a8-455a5273a522",
                    Username = "user2"
                }
            };
        }
    }
}
