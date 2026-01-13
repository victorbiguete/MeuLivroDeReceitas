using CommomTestsUtilities.BlobStorage;
using CommomTestsUtilities.Entities;
using CommomTestsUtilities.LoggedUser;
using CommomTestsUtilities.Mapper;
using CommomTestsUtilities.Repositories;
using CommomTestsUtilities.Requests;
using FluentAssertions;
using MyRecipeBook.Application.UseCases.Recipe.Filter;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCases.Test.Recipe.Filter
{
    public class FilterRecipeUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, _) = UserBuilder.Build();

            var request = RequestFilterRecipeJsonBuilder.Build();

            var recipes = RecipeBuilder.Collection(user);

            var useCase = CreateUseCase(user,recipes);

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Recipes.Should().NotBeNullOrEmpty();
            result.Recipes.Should().HaveCount(recipes.Count);
        }

        [Fact]
        public async Task Error_CookingTime_Invalid()
        {
            (var user, _) = UserBuilder.Build();

            var recipes = RecipeBuilder.Collection(user);

            var request = RequestFilterRecipeJsonBuilder.Build();
            request.CookingTime.Add((MyRecipeBook.Communication.Enum.CookingTime)1000);

            var useCase = CreateUseCase(user, recipes);

            Func<Task> act = async () => { await useCase.Execute(request); };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorsMessages.Count == 1 && e.ErrorsMessages.Contains(ResourceMessagesExceptions.COOKING_TIME_NOT_SUPPORTED));
        }

        private static FilterRecipeUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User user, IList<MyRecipeBook.Domain.Entities.Recipe> recipes)
        {
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var repository = new RecipeReadOnlyRepositoryBuilder().Filter(user, recipes).Build();
            var blobStorage = new BlobStorageServiceBuilder().GetFileUrl(user,recipes).Build();

            return new FilterRecipeUseCase(mapper, loggedUser, repository,blobStorage);
        }
    }
}
