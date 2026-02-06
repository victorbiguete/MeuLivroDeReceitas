using CommomTestsUtilities.BlobStorage;
using CommomTestsUtilities.Entities;
using CommomTestsUtilities.LoggedUser;
using CommomTestsUtilities.Repositories;
using CommomTestsUtilities.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MyRecipeBook.Application.UseCases.Recipe.Image;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCases.Test.Recipe.InlineData;

namespace UserCases.Test.Image
{
    public class AddUpdateImageCoverUseCaseTest
    {
        [Theory]
        [ClassData(typeof(ImageTypeInlineData))]
        public async Task Success(IFormFile file)
        {
            (var user, _) = UserBuilder.Build();
            var recipe = RecipeBuilder.Build(user);

            var useCase = CreateUseCase(user, recipe);

            Func<Task> act = async () => await useCase.Execute(recipe.Id, file);

            await act.Should().NotThrowAsync();
        }

        [Theory]
        [ClassData(typeof(ImageTypeInlineData))]
        public async Task Success_Recipe_Did_Not_Have_Image(IFormFile file)
        {
            (var user, _) = UserBuilder.Build();
            var recipe = RecipeBuilder.Build(user);
            recipe.ImageIdentifier = null;

            var useCase = CreateUseCase(user, recipe);

            Func<Task> act = async () => await useCase.Execute(recipe.Id, file);

            await act.Should().NotThrowAsync();

            recipe.ImageIdentifier.Should().NotBeNullOrWhiteSpace();
        }

        [Theory]
        [ClassData(typeof(ImageTypeInlineData))]
        public async Task Error_Recipe_NotFound(IFormFile file)
        {
            (var user, _) = UserBuilder.Build();

            var useCase = CreateUseCase(user);

            var act = async () => await useCase.Execute(1, file);

            (await act.Should().ThrowAsync<NotFoundException>())
                .Where(e => e.Message.Equals(ResourceMessagesExceptions.RECIPE_NOT_FOUND));
        }

        [Fact]
        public async Task Error_File_Is_Txt()
        {
            (var user, _) = UserBuilder.Build();
            var recipe = RecipeBuilder.Build(user);

            var useCase = CreateUseCase(user, recipe);

            var file = FormFileBuilder.Txt();

            var act = async () => await useCase.Execute(recipe.Id, file);

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorsMessages.Count == 1 &&
                    e.ErrorsMessages.Contains(ResourceMessagesExceptions.ONLY_IMAGES_ACCEPTED));
        }

        private static AddUpdateImageCoverUseCase CreateUseCase(
            MyRecipeBook.Domain.Entities.User user,
            MyRecipeBook.Domain.Entities.Recipe? recipe = null)
        {
            var loggedUser = LoggedUserBuilder.Build(user);
            var repository = new RecipeUpdateOnlyRepositoryBuilder().GetById(user, recipe).Build();
            var blobStorage = new BlobStorageServiceBuilder().Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new AddUpdateImageCoverUseCase(loggedUser, repository, unitOfWork, blobStorage);

        }
    }
}
