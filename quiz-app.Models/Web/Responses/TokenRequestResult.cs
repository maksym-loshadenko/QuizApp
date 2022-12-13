using quiz_app.Models.Web.ResponseModels;

namespace quiz_app.Models.Web.Responses
{
    public class TokenRequestResult: RequestResultBase
    {
        public Token? Result { get; set; } = new();
    }
}
