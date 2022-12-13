namespace quiz_app.Models.Database.Auth
{
    public class User
    {
        public string Username { get; set; } = "";

        public string HashedPassword { get; set; } = "";

        public string Salt { get; set; } = "";
    }
}
