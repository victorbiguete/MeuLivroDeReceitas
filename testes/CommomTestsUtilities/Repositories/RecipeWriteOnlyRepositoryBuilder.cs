using Moq;
using MyRecipeBook.Domain.Repositories.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommomTestsUtilities.Repositories
{
    public class RecipeWriteOnlyRepositoryBuilder
    {
        public static IRecipeWriteOnlyRepository Build()
        {
            var mock = new Mock<IRecipeWriteOnlyRepository>();

            return mock.Object;
        }
    }
}
