using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using quiz_app.DbMock;
using quiz_app.Logic;
using quiz_app.Models.Auth;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using quiz_app.Models.Web;
using quiz_app.Models.Web.Responses;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace quiz_app.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("/sign-in")]
        public TokenRequestResult SignIn(User user)
        {
            var result = new TokenRequestResult();

            var foundUser = Users.GetList().FirstOrDefault(u => u.Username == user.Username);

            if (foundUser == null)
            {
                result.SetError("Invalid user credentials.");
                result.Result = null;

                Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                return result;
            }

            if (!Auth.ComparePassword(user.Password, foundUser.HashedPassword, foundUser.Salt))
            {
                result.SetError("Invalid user credentials.");
                result.Result = null;

                Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                return result;
            }

            var issuer = _config["Security:Issuer"];
            var audience = _config["Security:Audience"];
            var key = Encoding.UTF8.GetBytes(_config["Security:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature
                )
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            result.Result.AuthToken = jwtToken;

            return result;
        }
    }
}
