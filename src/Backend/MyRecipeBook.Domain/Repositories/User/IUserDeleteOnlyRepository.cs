using MyRecipeBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Domain.Repositories.User
{
    public interface IUserDeleteOnlyRepository
    {
        Task DeleteAccount(Guid userIdentifier);
    }
}
