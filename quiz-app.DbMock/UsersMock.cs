using quiz_app.Models.Database.Auth;

namespace quiz_app.DbMock
{
    public class UsersMock
    {
        public static List<User> GetList()
        {
            return new List<User>()
            {
                new()
                {
                    Username = "user1",
                    HashedPassword = "8251EE4D300ED63949830E8741EE27D468467C6B18118718CBAEEACD8FEE2D26",
                    Salt = "+velff0urNIgU37qvr/5tIHZSZeZTk/X+oXm36k/2zLXW9BknqRvOirXRafwNRAOat2ss186TykWkU7tLMzoHQ=="
                },
                new()
                {
                    Username = "user2",
                    HashedPassword = "955E2F94D2646F35DA044DCFF0A2E74D1A48FE1B1B1896940EC8255A875FFEBA",
                    Salt = "0BRFR6UYvs2JvzrovtTHNQI2oB1qcrWc/XUHNTpNhewYvejh9YNSfNzxGEqySpQXQ1mzT5hxItJICbEXoFfJdA=="
                }
            };
        }
    }
}
