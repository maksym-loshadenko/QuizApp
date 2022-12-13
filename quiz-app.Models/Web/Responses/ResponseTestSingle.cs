using quiz_app.Models.Web.Responses.Models.Tests;

namespace quiz_app.Models.Web.Responses
{
    public class ResponseTestSingle: ResponseBase
    {
        public TestModel? Test { get; set; } = new();
    }
}
