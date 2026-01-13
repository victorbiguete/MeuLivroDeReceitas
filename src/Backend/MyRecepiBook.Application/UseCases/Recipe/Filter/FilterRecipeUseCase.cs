using AutoMapper;
using Microsoft.Extensions.Options;
using MyRecipeBook.Application.Extensions;
using MyRecipeBook.Communication.Enum;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Response;
using MyRecipeBook.Domain.Dtos;
using MyRecipeBook.Domain.Enum;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Domain.Services.Storage;
using MyRecipeBook.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.UseCases.Recipe.Filter
{
    public class FilterRecipeUseCase : IFilterRecipeUseCase
    {
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly IRecipeReadOnlyRepository _repository;
        private readonly IBlobStorageService _blobStorageService;

        public FilterRecipeUseCase(IMapper mapper, ILoggedUser loggedUser, IRecipeReadOnlyRepository repository, IBlobStorageService blobStorageService)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
            _repository = repository;
            _blobStorageService = blobStorageService;
        }

        public async Task<ResponseRecipesJson> Execute(RequestFilterRecipeJson request)
        {
            Validate(request);

            var loggedUser = await _loggedUser.User();

            var filters = new FilterRecipesDto{
                RecipeTitle_Ingredient = request.RecipeTitle_Ingredient,
                CookingTimes = request.CookingTime.Distinct().Select(c => (Domain.Enum.CookingTime)c).ToList(),
                Difficulties = request.Difficulties.Distinct().Select(d => (Domain.Enum.Difficulty)d).ToList(),
                DishTypes = request.DishTypes.Distinct().Select(dt => (Domain.Enum.DishType)dt).ToList(),
            };

            var recipes = await _repository.Filter(loggedUser, filters); 

            return new ResponseRecipesJson
            {
                Recipes = await recipes.MapToShortRecipeJson(loggedUser, _blobStorageService, _mapper)
            };
        }

        private static void Validate(RequestFilterRecipeJson request)
        {
            var validator = new FilterRecipeValidator();

            var result = validator.Validate(request);

            if(!result.IsValid)
            {
                var errorMessage = result.Errors.Select(error => error.ErrorMessage).Distinct().ToList();

                throw new ErrorOnValidationException(errorMessage);
            }
        }
    }
}
