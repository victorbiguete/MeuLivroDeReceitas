using MyRecipeBook.Application.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommomTestsUtilities.Cryptography
{
    public static class PasswordEncripterBuilder
    {
        public static PasswordEncripter Build()
        {
            return new PasswordEncripter("ABC1234");
        }
    }
}
