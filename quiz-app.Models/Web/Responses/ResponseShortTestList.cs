using quiz_app.Models.Web.Responses.Models.Tests;

namespace quiz_app.Models.Web.Responses
{
    public class ResponseShortTestList: ResponseBase
    {
        public List<ShortTestModel?> Tests { get; set; } = new();
    }
}
