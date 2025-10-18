using MyRecipeBook.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.UseCases.Recipe.Update
{
    internal interface IUpdateRecipeUseCase
    {
        public Task Execute(long recipeId, RequestRecipeJson request);
    }
}
