using quiz_app.Models.Web.Responses.Models.Auth;

namespace quiz_app.Models.Web.Responses
{
    public class ResponseAuthToken: ResponseBase
    {
        public TokenModel? Result { get; set; } = new();
    }
}
