using quiz_app.Models.Web.Responses.Models.Tests;

namespace quiz_app.Models.Web.Responses
{
    public class ResponseShortTestSingle: ResponseBase
    {
        public ShortTestModel? Test { get; set; } = new();
    }
}
