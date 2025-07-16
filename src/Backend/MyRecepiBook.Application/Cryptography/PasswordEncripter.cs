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
        public string Encrypt(string password)
        {
            var chaveAdicional = "ABC";
            var newPassword = $"{password}{chaveAdicional}";

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
