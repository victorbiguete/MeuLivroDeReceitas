using AutoMapper;
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

namespace MyRecipeBook.Application.UseCases.Recipe.Delete
{
    public class DeleteRecipeUseCase : IDeleteRecipeUseCase
    {
        private readonly IRecipeReadOnlyRepository _repositoryRead;
        private readonly ILoggedUser _loggedUser;
        private readonly IRecipeWriteOnlyRepository _repositoryWrite;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRecipeUseCase(IRecipeReadOnlyRepository repositoryRead, ILoggedUser loggedUser, IRecipeWriteOnlyRepository repositoryWrite, IUnitOfWork unitOfWork)
        {
            _repositoryRead = repositoryRead;
            _loggedUser = loggedUser;
            _repositoryWrite = repositoryWrite;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long recipeId)
        {
            var loggedUser = await _loggedUser.User();

            var recipe = await _repositoryRead.GetById(loggedUser, recipeId);

            if (recipe is null)
                throw new NotFoundException(ResourceMessagesExceptions.RECIPE_NOT_FOUND);

            await _repositoryWrite.Delete(recipeId);

            await _unitOfWork.Commit();
        }
    }
}
