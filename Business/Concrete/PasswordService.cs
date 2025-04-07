// Business/Concrete/PasswordService.cs

using Org.BouncyCastle.Crypto.Generators;

namespace Business.Concrete
{
    public class PasswordService
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
