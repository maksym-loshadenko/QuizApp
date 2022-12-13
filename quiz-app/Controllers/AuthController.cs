using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using quiz_app.DbMock;
using quiz_app.Logic;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using quiz_app.Models.Web.Responses;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using Microsoft.AspNetCore.Authorization;
using quiz_app.Models.Web.Requests;

namespace quiz_app.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IConfiguration config, ILogger<AuthController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpPost("/auth/sign-in")]
        [AllowAnonymous]
        public ResponseAuthToken SignIn(RequestAuth user)
        {
            var result = new ResponseAuthToken();

            try
            {
                var foundUser = AuthLogic.GetUserByUsername(user.Username);

                if (!AuthLogic.ComparePassword(user.Password, foundUser.HashedPassword, foundUser.Salt))
                    throw new Exception("Invalid user credentials.");

                var issuer = _config["Security:Issuer"];
                var audience = _config["Security:Audience"];
                var key = Encoding.UTF8.GetBytes(_config["Security:Key"]);

                result.Result.AuthToken = AuthLogic.GenerateJwtToken(user, issuer, audience, key) ??
                    throw new Exception("Unable to generate JWT tokens");
            }
            catch(Exception ex)
            {
                result.SetError(ex.GetBaseException().Message);
                result.Result = null;

                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }

            return result;
        }
    }
}
