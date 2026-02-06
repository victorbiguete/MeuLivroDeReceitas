using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.UseCases.Login.External
{
    public interface IExternalLoginUseCase
    {
        Task<string> Execute(string name, string email);
    }
}
