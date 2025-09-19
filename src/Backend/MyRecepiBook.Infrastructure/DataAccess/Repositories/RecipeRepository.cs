using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories
{
    public class RecipeRepository : IRecipeWriteOnlyRepository
    {
        private readonly AppDbContext _context;

        public RecipeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Recipe recipe) => await _context.Recipes.AddAsync(recipe);
    }
}
