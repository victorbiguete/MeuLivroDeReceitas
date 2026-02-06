using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.UseCases.Recipe.GenerateRecipe
{
    public interface IGenerateRecipeUseCase
    {
        public Task<ResponseGenerateRecipeJson> Execute(RequestGenerateRecipeJson request);
    }
}
