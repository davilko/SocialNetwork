using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SocialNetwork.Utility.Cryptography
{
    public static class Cryptography
    {
        private static string[] Salt => new [] { "9a", "8e", "cc", "4a", "53", "d1", "c8", "7d", "e3", "a7" };

        public static string ToSHA256(this string input)
        {
            var salt = Salt.Select(s => Convert.ToByte(s)).ToArray();
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hashedInput = sha.ComputeHash(bytes);
                var resultWithSalt = new List<byte>(hashedInput.Length + salt.Length);
                resultWithSalt.AddRange(salt);
                resultWithSalt.AddRange(bytes);
                return Convert.ToBase64String(resultWithSalt.ToArray());
            }
        }
    }
}