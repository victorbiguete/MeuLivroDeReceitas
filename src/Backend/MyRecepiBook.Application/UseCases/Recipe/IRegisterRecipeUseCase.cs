using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.UseCases.Recipe
{
    public interface IRegisterRecipeUseCase
    {
        public Task<ResponseRegisteredRecipeJson> Execute(RequestRecipeJson requestRecipeJson);
    }
}
