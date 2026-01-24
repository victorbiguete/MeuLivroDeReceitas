using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository, IUserDeleteOnlyRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            
        }

        public async Task DeleteAccount(Guid userIdentifier)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.UserIdentifier == userIdentifier);

            if (user is null)
                return;

            var recipes = _context.Recipes.Where(recipe => recipe.UserId == user.Id);

            _context.Recipes.RemoveRange(recipes);

            _context.Users.Remove(user);
        }

        public async Task<bool> ExistActiveUserWithEmail(string email)
        {
            return await _context.Users.AnyAsync(u => u.Active && u.Email.Equals(email));
        }

        public async Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier)
        {
            return await _context.Users.AnyAsync(user => user.UserIdentifier.Equals(userIdentifier) && user.Active);
        }

        public async Task<User?> GetByEmailAndPassword(string email, string password)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Active && user.Email.Equals(email) && user.Password.Equals(password));
        }

        public async Task<User> GetById(long id)
        {
            return await _context.Users.FirstAsync(user => user.Id == id);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }
    }
}
