using CommomTestsUtilities.BlobStorage;
using CommomTestsUtilities.Entities;
using CommomTestsUtilities.LoggedUser;
using CommomTestsUtilities.Mapper;
using CommomTestsUtilities.Repositories;
using CommomTestsUtilities.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MyRecipeBook.Application.UseCases.Recipe;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCases.Test.Recipe.InlineData;

namespace UserCases.Test.Recipe.Register
{
    public class RegisterRecipeUseCaseText
    {
        [Fact]
        public async Task Sucess()
        {
            var (user, _) = UserBuilder.Build();

            var request = RequestRegisterRecipeFormDataBuilder.Build();

            var useCase =  CreateUseCase(user);

            var result =  await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Id.Should().NotBeNullOrWhiteSpace();
            result.Title.Should().Be(request.Title);
        }

        [Theory]
        [ClassData(typeof(ImageTypeInlineData))]
        public async Task Success_With_Image(IFormFile file)
        {
            (var user, _) = UserBuilder.Build();

            var request = RequestRegisterRecipeFormDataBuilder.Build(file);

            var useCase = CreateUseCase(user);

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Id.Should().NotBeNullOrWhiteSpace();
            result.Title.Should().Be(request.Title);
        }

        [Fact]
        public async Task Error_Title_Empty()
        {
            var (user, _) = UserBuilder.Build();

            var request = RequestRegisterRecipeFormDataBuilder.Build();
            request.Title = string.Empty;

            var useCase = CreateUseCase(user);

            Func<Task> act = async () => { await useCase.Execute(request); };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorsMessages.Count == 1 &&
                e.ErrorsMessages.Contains(ResourceMessagesExceptions.RECIPE_TITLE_EMPTY));
        }

        [Fact]
        public async Task Error_Invalid_File()
        {
            (var user, _) = UserBuilder.Build();

            var textFile = FormFileBuilder.Txt();

            var request = RequestRegisterRecipeFormDataBuilder.Build(textFile);

            var useCase = CreateUseCase(user);

            var act = async () => { await useCase.Execute(request); };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorsMessages.Count == 1 &&
                    e.ErrorsMessages.Contains(ResourceMessagesExceptions.ONLY_IMAGES_ACCEPTED));
        }

        private static RegisterRecipeUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User user)
        {
            var mapper = MapperBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var repository = RecipeWriteOnlyRepositoryBuilder.Build();
            var blobStorage = new BlobStorageServiceBuilder().Build();

            return new RegisterRecipeUseCase(loggedUser, repository, unitOfWork, mapper, blobStorage);
        }
    }
}
