using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Domain.Security.Cryptography
{
    public interface IPasswordEncripter
    {
        public string Encrypt(string password);
    }
}
