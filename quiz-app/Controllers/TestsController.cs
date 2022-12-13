using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using quiz_app.Logic;
using quiz_app.Models.Web.Responses;
using System.Net;
using System.Numerics;
using quiz_app.Models.Web;
using quiz_app.Models.Web.Requests;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace quiz_app.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        public TestsController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        [HttpPost("/test/check")]
        [Authorize]
        public ResponseTestCheck CheckTest(RequestCheckTest checkTest)
        {
            var result = new ResponseTestCheck();

            var username = User.FindFirst("sub")?.Value ?? throw new Exception("Invalid access token");

            if (!TestsLogic.DoesHaveAccessToTest(username, checkTest.TestId))
            {
                result.SetError("You don't have access to this test.");
                result.Score = null;

                Response.StatusCode = (int)HttpStatusCode.Forbidden;

                return result;
            }

            try
            {
                result.Score = TestsLogic.GetTestScore(checkTest);
            }
            catch (Exception ex)
            {
                result.SetError(ex.GetBaseException().Message);
                result.Score = null;

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return result;
        }

        [HttpGet("/test")]
        [Authorize]
        public ResponseTestSingle GetTestById(string id)
        {
            var result = new ResponseTestSingle();

            var username = User.FindFirst("sub")?.Value ?? throw new Exception("Invalid access token");

            if (!TestsLogic.DoesHaveAccessToTest(username, id))
            {
                result.SetError("You don't have access to this test.");
                result.Test = null;

                Response.StatusCode = (int)HttpStatusCode.Forbidden;

                return result;
            }
                
            try
            {
                result.Test = TestsLogic.GetTestById(id);
            } 
            catch (Exception ex)
            {
                result.SetError(ex.GetBaseException().Message);
                result.Test = null;

                Response.StatusCode = (int)HttpStatusCode.NotFound;
            }

            return result;
        }

        [HttpGet("/tests")]
        [Authorize]
        public ResponseShortTestList GetAllAvailableTests()
        {
            var result = new ResponseShortTestList();

            var username = User.FindFirst("sub")?.Value ?? throw new Exception("Invalid access token");

            try
            {
                result.Tests = TestsLogic.GetAvailableTests(username);
            }
            catch (Exception ex)
            {
                result.SetError(ex.GetBaseException().Message);
                result.Tests = null;

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return result;
        }
    }
}
