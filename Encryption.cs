using System;
using System.Text;

namespace MobileOperator
{
    internal class Encryption
    {
        public string PasswordEncrypt(string password, string salt)
        {
            var sha256 = System.Security.Cryptography.SHA256.Create();
            var saltedPasswordAsBytes = Encoding.UTF8.GetBytes($"{salt}{password}");

            return Convert.ToBase64String(sha256.ComputeHash(saltedPasswordAsBytes));
        }
    }
}