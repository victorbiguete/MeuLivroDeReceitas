using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using Microsoft.AspNetCore.Http;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.UseCases.Recipe.Image
{
    public class AddUpdateImageCoverUseCase : IAddUpdateImageCoverUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IRecipeUpdateOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public AddUpdateImageCoverUseCase(ILoggedUser loggedUser, IRecipeUpdateOnlyRepository repository, IUnitOfWork unitOfWork)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long recipeId, IFormFile file)
        {
            var loggedUser = await _loggedUser.User();

            var recipe = await _repository.GetById(loggedUser,recipeId);

            if ((recipe is null))
            {
                throw new NotFoundException(ResourceMessagesExceptions.RECIPE_NOT_FOUND);
            }

            var fileStream = file.OpenReadStream();

            if(!fileStream.Is<PortableNetworkGraphic>() && !fileStream.Is<JointPhotographicExpertsGroup>())
            {
                throw new ErrorOnValidationException([
                    ResourceMessagesExceptions.ONLY_IMAGES_ACCEPTED]
                );
            }
        }
    }
}
