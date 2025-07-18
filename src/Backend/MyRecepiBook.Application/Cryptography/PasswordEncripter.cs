using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.Cryptography
{
    public class PasswordEncripter
    {
        private readonly string _additionalKey;

        public PasswordEncripter(string additionalKey)
        {
            _additionalKey = additionalKey;
        }

        public string Encrypt(string password)
        {
            var newPassword = $"{password}{_additionalKey}";

            var bytes = Encoding.UTF8.GetBytes(password);
            var hashByte = SHA512.HashData(bytes);

            return StringBytes(hashByte);
        }

        private static string StringBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach(byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
    }
}
