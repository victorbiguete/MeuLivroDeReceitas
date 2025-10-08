using AutoMapper;
using Microsoft.Extensions.Options;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Response;
using MyRecipeBook.Domain.Services.LoggedUser;
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

        public FilterRecipeUseCase(IMapper mapper, ILoggedUser loggedUser)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
        }

        public async Task<ResponseRecipesJson> Execute(RequestFilterRecipeJson request)
        {
            Validate(request);

            var loggedUser = await _loggedUser.User();

            return new ResponseRecipesJson
            {
                Recipes = []
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
