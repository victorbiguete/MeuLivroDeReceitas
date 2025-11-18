using AutoMapper;
using MyRecipeBook.Communication.Response;
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

namespace MyRecipeBook.Application.UseCases.Recipe.GetById
{
    public class GetRecipeByIdUseCase : IGetRecipeByIdUseCase
    {
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly IRecipeReadOnlyRepository _repository;
        private readonly IBlobStorageService _blobStorageService;

        public GetRecipeByIdUseCase(IMapper mapper, ILoggedUser loggedUser, IRecipeReadOnlyRepository repository, IBlobStorageService blobStorageService)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
            _repository = repository;
            _blobStorageService = blobStorageService;
        }

        public async Task<ResponseRecipeJson> Execute(long recipeID)
        {
            var loggedUser = await _loggedUser.User();

            var recipe = await _repository.GetById(loggedUser,recipeID);

            if(recipe is null)
            {
                throw new NotFoundException(ResourceMessagesExceptions.RECIPE_NOT_FOUND);
            }
            var response = _mapper.Map<ResponseRecipeJson>(recipe);

            if (!string.IsNullOrEmpty(recipe.ImageIdentifier))
            {
                var url = await _blobStorageService.GetImageUrl(loggedUser, recipe.ImageIdentifier);

                response.ImageUrl = url;
            }
            return response;
        }
    }
}
