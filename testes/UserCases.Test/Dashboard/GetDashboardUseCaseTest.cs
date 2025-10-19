using CommomTestsUtilities.Entities;
using CommomTestsUtilities.LoggedUser;
using CommomTestsUtilities.Mapper;
using CommomTestsUtilities.Repositories;
using FluentAssertions;
using MyRecipeBook.Application.UseCases.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCases.Test.Dashboard
{
    public class GetDashboardUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, _) = UserBuilder.Build();
            var recipes = RecipeBuilder.Collection(user);

            var useCase = CreateUseCase(user,recipes);

            var result = await useCase.Execute();

            result.Should().NotBeNull();
            result.Recipes.Should().HaveCountGreaterThan(0).And.OnlyHaveUniqueItems(r => r.Id).And.AllSatisfy(recipe =>
            {
                recipe.Id.Should().NotBeNullOrWhiteSpace();
                recipe.Title.Should().NotBeNullOrWhiteSpace();
                recipe.AmountIngredients.Should().BeGreaterThan(0);
            });
        }

        private static GetDashboardUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User user, IList<MyRecipeBook.Domain.Entities.Recipe> recipes)
        {
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var repository = new RecipeReadOnlyRepositoryBuilder().GetForDashboard(user,recipes).Build();

            return new GetDashboardUseCase(repository,mapper,loggedUser);
        }
    }
}
