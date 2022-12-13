using quiz_app.DbMock;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using System.Security.Claims;
using quiz_app.Models.Web.Requests;

namespace quiz_app.Logic
{
    public class AuthLogic
    {
        private const int KeySize = 64;

        public static string? GenerateJwtToken(RequestAuth user, string? issuer, string? audience, byte[]? key)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static Models.Database.Auth.User GetUserByUsername(string username)
        {
            var foundUser = UsersMock.GetList().FirstOrDefault(u => u.Username == username);

            if (foundUser == null)
                throw new Exception("Invalid user credentials.");

            return foundUser;
        }

        private static string CreateSalt(int size)
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(size));
        }

        public static KeyValuePair<string, string> HashPassword(string password, string? salt = null)
        {
            salt ??= CreateSalt(KeySize);

            var bytes = Encoding.UTF8.GetBytes(password + salt);
            var sha256HashString = SHA256.HashData(bytes);

            return new KeyValuePair<string, string>(Convert.ToHexString(sha256HashString), salt);
        }

        public static bool ComparePassword(string password, string hash, string salt)
        {
            return hash == HashPassword(password, salt).Key;
        }
    }
}