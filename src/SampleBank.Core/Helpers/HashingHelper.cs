using System.Linq;
using System.Text;

namespace SampleBank.Core.Helpers
{
    public class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return !computeHash.Where((t, i) => t != passwordHash[i]).Any();
        }
    }
}