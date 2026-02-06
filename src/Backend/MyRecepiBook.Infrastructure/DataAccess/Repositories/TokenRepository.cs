using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly AppDbContext _context;

        public TokenRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken?> Get(string refreshToken)
        {
            return await _context.RefreshTokens
                .AsNoTracking()
                .Include(token => token.User)
                .FirstOrDefaultAsync(token => token.Value.Equals(refreshToken));
        }

        public async Task SaveNewRefreshToken(RefreshToken refreshToken)
        {
            var tokens = _context.RefreshTokens.Where(token => token.UserId == refreshToken.UserId);

            _context.RefreshTokens.RemoveRange(tokens);

            await _context.RefreshTokens.AddAsync(refreshToken);
        }
    }
}
