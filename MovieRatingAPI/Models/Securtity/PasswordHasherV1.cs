using MovieRatingAPI.Interfaces.Security;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace MovieRatingAPI.Models.Securtity
{
    public class PasswordHasherV1 : IPasswordHasher
    {
        private const int SaltSize = 32;
        private const int KeySize = 32;
        private const int Iterations = 100000;

        public string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                Iterations,
                HashAlgorithmName.SHA256,
                KeySize
                );

            string saltBase64 = Convert.ToBase64String(salt);
            string hashBase64 = Convert.ToBase64String(hash);

            return $"v=1;algo=pdkdf2;iter={Iterations};salt={saltBase64};hash={hashBase64}";
        }

        public bool VerifyPassword(string password, string storedHash)
        {
            var parts = storedHash.Split(';');

            var iterPart = parts.First(p => p.StartsWith("iter=")).Substring(5);
            var saltPart = parts.First(p => p.StartsWith("salt=")).Substring(5);
            var hashPart = parts.First(p => p.StartsWith("hash=")).Substring(5);

            int iterations = int.Parse(iterPart);
            byte[] salt = Convert.FromBase64String(saltPart);
            byte[] expectedHash = Convert.FromBase64String(hashPart);

            byte[] actualHash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                HashAlgorithmName.SHA256,
                expectedHash.Length
                );

            return CryptographicOperations.FixedTimeEquals(actualHash,expectedHash);
        }
    }
}
