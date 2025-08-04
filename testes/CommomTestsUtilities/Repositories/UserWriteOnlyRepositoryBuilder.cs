using Moq;
using MyRecipeBook.Domain.Repositories.User;

namespace CommomTestsUtilities.Repositories
{
    public static class UserWriteOnlyRepositoryBuilder
    {
        public static IUserWriteOnlyRepository Build()
        {
            var mock = new Mock<IUserWriteOnlyRepository>();

            return mock.Object;
        }
    }
}
