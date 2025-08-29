using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Infrastructure.Security.Tokens.Access
{
    public abstract class JwtTokenHandler
    {
        protected SymmetricSecurityKey SecurityKey(string signingKey)
        {
            var bytes = Encoding.UTF8.GetBytes(signingKey);
            return new SymmetricSecurityKey(bytes);
        }
    }
}
