using static System.Runtime.InteropServices.JavaScript.JSType;

namespace quiz_app.Models.Web
{
    public class RequestResultBase
    {
        public Error Error { get; set; } = new();

        public void SetError(string message)
        {
            Error.IsError = true;
            Error.Message = message;
        }
    }
}
