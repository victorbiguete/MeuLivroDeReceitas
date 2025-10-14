using AutoMapper;
using MyRecipeBook.Communication.Response;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services.LoggedUser;
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

        public GetRecipeByIdUseCase(IMapper mapper, ILoggedUser loggedUser, IRecipeReadOnlyRepository repository)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
            _repository = repository;
        }

        public async Task<ResponseRecipesJson> Execute(long recipeID)
        {
            var loggedUser = await _loggedUser.User();

            var recipe = await _repository.GetById(loggedUser,recipeID);

            if(recipe is null)
            {
                throw new NotFoundException(ResourceMessagesExceptions.RECIPE_NOT_FOUND);
            }
            return _mapper.Map<ResponseRecipesJson>(recipe);
        }
    }
}
