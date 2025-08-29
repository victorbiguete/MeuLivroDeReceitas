using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Domain.Security.Tokens
{
    public interface IAccessTokenValidator
    {
        public Guid ValidateAndGetUserIdentifier(string token);
    }
}
