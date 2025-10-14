using MyRecipeBook.Communication.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.UseCases.Recipe.GetById
{
    public interface IGetRecipeByIdUseCase
    {
        Task<ResponseRecipesJson> Execute(long recipeID);
    }
}
