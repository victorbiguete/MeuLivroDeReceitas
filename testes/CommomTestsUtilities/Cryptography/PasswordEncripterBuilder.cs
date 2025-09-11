using MyRecipeBook.Domain.Security.Cryptography;
using MyRecipeBook.Infrastructure.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommomTestsUtilities.Cryptography
{
    public static class PasswordEncripterBuilder
    {
        public static IPasswordEncripter Build()
        {
            return new Shar512Encripter("ABC1234");
        }
    }
}
