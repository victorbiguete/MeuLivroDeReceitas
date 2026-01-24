using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.UseCases.User.Delete.Delete
{
    public interface IDeleteUserAccountUseCase
    {
        Task Execute(Guid userIdentifier);
    }
}
