using MyRecipeBook.Domain.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Infrastructure.Security.Cryptography
{
    public class BCryptNet : IPasswordEncripter
    {
        public string Encrypt(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool IsValid(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password,passwordHash);
        }
    }
}
