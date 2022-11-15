using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace MobileOperator
{
    /// <summary>
    /// Encryptor.
    /// </summary>
    internal class Encryptor
    {
        /// <summary>
        /// Password еncrypt.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        internal string PasswordEncrypt(string password, string salt)
        {
            var sha256 = SHA256.Create();
            var saltedPasswordAsBytes = Encoding.UTF8.GetBytes($"{salt}{password}");
            var hash = Convert.ToBase64String(sha256.ComputeHash(saltedPasswordAsBytes));

            return hash;
        }

        internal string GetHashSalt()
        {
            Random rand = new Random();

            string str = "abcdefghijklmnopqrstuvwxyz" +
                         "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                         "0123456789";

            string salt = "";

            for (int len = 0; len < 10; len++)
            {
                int letter = rand.Next(62);

                salt += str[letter];
            }

            return salt;
        }
    }
}