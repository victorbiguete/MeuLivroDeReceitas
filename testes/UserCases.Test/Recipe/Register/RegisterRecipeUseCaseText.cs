using CommomTestsUtilities.Entities;
using CommomTestsUtilities.LoggedUser;
using CommomTestsUtilities.Mapper;
using CommomTestsUtilities.Repositories;
using CommomTestsUtilities.Requests;
using FluentAssertions;
using MyRecipeBook.Application.UseCases.Recipe;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCases.Test.Recipe.Register
{
    public class RegisterRecipeUseCaseText
    {
        [Fact]
        public async Task Sucess()
        {
            var (user, _) = UserBuilder.Build();

            var request = RequestRecipeJsonBuilder.Build();

            var useCase =  CreateUseCase(user);

            var result =  await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Id.Should().NotBeNullOrWhiteSpace();
            result.Title.Should().Be(request.Title);
        }

        [Fact]
        public async Task Error_Title_Empty()
        {
            var (user, _) = UserBuilder.Build();

            var request = RequestRecipeJsonBuilder.Build();
            request.Title = string.Empty;

            var useCase = CreateUseCase(user);

            Func<Task> act = async () => { await useCase.Execute(request); };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorsMessages.Count == 1 &&
                e.ErrorsMessages.Contains(ResourceMessagesExceptions.RECIPE_TITLE_EMPTY));
        }

        private static RegisterRecipeUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User user)
        {
            var mapper = MapperBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var repository = RecipeWriteOnlyRepositoryBuilder.Build();

            return new RegisterRecipeUseCase(loggedUser, repository, unitOfWork, mapper);
        }
    }
}
