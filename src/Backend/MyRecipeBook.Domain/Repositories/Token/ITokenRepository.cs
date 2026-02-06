using MyRecipeBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Domain.Repositories.Token
{
    public interface ITokenRepository
    {
        Task<RefreshToken?> Get(string refreshToken);
        Task SaveNewRefreshToken(RefreshToken refreshToken);
    }
}
