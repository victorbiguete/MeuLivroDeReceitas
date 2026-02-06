using AutoMapper;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Domain.Services.Storage;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.UseCases.Recipe.Delete
{
    public class DeleteRecipeUseCase : IDeleteRecipeUseCase
    {
        private readonly IRecipeReadOnlyRepository _repositoryRead;
        private readonly ILoggedUser _loggedUser;
        private readonly IRecipeWriteOnlyRepository _repositoryWrite;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;

        public DeleteRecipeUseCase(IRecipeReadOnlyRepository repositoryRead, ILoggedUser loggedUser, IRecipeWriteOnlyRepository repositoryWrite, IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
        {
            _repositoryRead = repositoryRead;
            _loggedUser = loggedUser;
            _repositoryWrite = repositoryWrite;
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;
        }

        public async Task Execute(long id)
        {
            var loggedUser = await _loggedUser.User();

            var recipe = await _repositoryRead.GetById(loggedUser, id);

            if (recipe is null)
                throw new NotFoundException(ResourceMessagesExceptions.RECIPE_NOT_FOUND);

            if (!string.IsNullOrEmpty(recipe.ImageIdentifier))
            {
                await _blobStorageService.Delete(loggedUser,recipe.ImageIdentifier);
            }

            await _repositoryWrite.Delete(id);

            await _unitOfWork.Commit();
        }
    }
}
