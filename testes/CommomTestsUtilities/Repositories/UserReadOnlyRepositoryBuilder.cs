using Moq;
using MyRecipeBook.Domain.Repositories.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommomTestsUtilities.Repositories
{
    public class UserReadOnlyRepositoryBuilder
    {
        private readonly Mock<IUserReadOnlyRepository> _repository;

        public UserReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<IUserReadOnlyRepository>();
        }

        public IUserReadOnlyRepository Build()
        {
            return _repository.Object;
        }

        public void ExistActiveUserWithEmail(string email)
        {
            _repository.Setup(repository => repository.ExistActiveUserWithEmail(email)).ReturnsAsync(true);
        }
    }
}
