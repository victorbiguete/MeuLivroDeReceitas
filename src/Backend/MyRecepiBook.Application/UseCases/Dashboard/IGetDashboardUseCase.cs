using MyRecipeBook.Communication.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.UseCases.Dashboard
{
    public interface IGetDashboardUseCase
    {
        public Task<ResponseRecipesJson> Execute();
    }
}
