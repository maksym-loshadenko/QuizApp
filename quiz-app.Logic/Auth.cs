using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace quiz_app.Logic
{
    public class Auth
    {
        private const int KeySize = 64;
        private const int Iterations = 350000;

        private static HashAlgorithmName _algorithm = HashAlgorithmName.SHA256;

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