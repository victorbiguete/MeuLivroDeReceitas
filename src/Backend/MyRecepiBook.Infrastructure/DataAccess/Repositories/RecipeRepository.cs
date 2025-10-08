using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Dtos;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories
{
    public class RecipeRepository : IRecipeWriteOnlyRepository, IRecipeReadOnlyRepository
    {
        private readonly AppDbContext _context;

        public RecipeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Recipe recipe) => await _context.Recipes.AddAsync(recipe);

        public async Task<IList<Recipe>> Filter(User user, FilterRecipesDto filters)
        {
            var query = _context.Recipes.AsNoTracking().Include(recipe => recipe.Ingredients).Where(recipe => recipe.Active && recipe.UserId == user.Id);

            if(filters.Difficulties.Any())
            {
                query = query.Where(recipe => recipe.Difficulty.HasValue && filters.Difficulties.Contains(recipe.Difficulty.Value));
            }
            if(filters.CookingTimes.Any())
            {
                query = query.Where(recipe => recipe.CookingTime.HasValue && filters.CookingTimes.Contains(recipe.CookingTime.Value));
            }
            if(filters.DishTypes.Any())
            {
                query = query.Where(recipe => recipe.DishTypes.Any(dishtype => filters.DishTypes.Contains(dishtype.Type)));
            }

            if(filters.RecipeTitle_Ingredient != string.Empty)
            {
                query = query.Where(recipes => recipes.Title.Contains(filters.RecipeTitle_Ingredient) || recipes.Ingredients.Any(ingredient => ingredient.Item.Contains(filters.RecipeTitle_Ingredient)));
            }
            
            return await query.ToListAsync();
        }
    }
}
