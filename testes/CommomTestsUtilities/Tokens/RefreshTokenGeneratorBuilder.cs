using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Infrastructure.Security.Tokens.Refresh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommomTestsUtilities.Tokens
{
    public class RefreshTokenGeneratorBuilder
    {
        public static IRefreshTokenGenerator Build() => new RefreshTokenGenerator();
    }
}
