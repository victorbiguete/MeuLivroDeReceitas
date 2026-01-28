using MyRecipeBook.Domain.Security.Cryptography;
using MyRecipeBook.Infrastructure.Security.Cryptography;

namespace CommomTestsUtilities.Cryptography
{
    public static class PasswordEncripterBuilder
    {
        public static IPasswordEncripter Build()
        {
            return new BCryptNet();
        }
    }
}
