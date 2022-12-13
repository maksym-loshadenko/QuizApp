using quiz_app.Models.Database;

namespace quiz_app.DbMock
{
    public class Users
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
            };
        }
    }
}
