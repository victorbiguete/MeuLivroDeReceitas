using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
    public class RecipeRepository : IRecipeWriteOnlyRepository, IRecipeReadOnlyRepository, IRecipeUpdateOnlyRepository
    {
        private readonly AppDbContext _context;

        public RecipeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Recipe recipe) => await _context.Recipes.AddAsync(recipe);

        public async Task Delete(long id)
        {
            var recipes = await _context.Recipes.FindAsync(id);

            _context.Recipes.Remove(recipes!);
        }

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

            if(filters.RecipeTitle_Ingredient != null)
            {
                query = query.Where(recipes => recipes.Title.Contains(filters.RecipeTitle_Ingredient) || recipes.Ingredients.Any(ingredient => ingredient.Item.Contains(filters.RecipeTitle_Ingredient)));
            }
            
            return await query.ToListAsync();
        }

        async Task<Recipe?> IRecipeReadOnlyRepository.GetById(User user, long recipeId)
        {
            return await GetFullRecipe()
                .AsNoTracking()
                .FirstOrDefaultAsync(recipe => recipe.Active && recipe.Id == recipeId && recipe.UserId == user.Id);
        }

        async Task<Recipe?> IRecipeUpdateOnlyRepository.GetById(User user, long recipeId)
        {
            return await GetFullRecipe()
                .FirstOrDefaultAsync(recipe => recipe.Active && recipe.Id == recipeId && recipe.UserId == user.Id);
        }
        public void Update(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
        }

        private IIncludableQueryable<Recipe, IList<Instruction>> GetFullRecipe()
        {
            return _context.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.DishTypes)
                .Include(r => r.Instructions);
        }

        public async Task<IList<Recipe>> GetForDashboard(User user)
        {
            return await _context.Recipes
                .AsNoTracking()
                .Include(c => c.Ingredients)
                .Where(r => r.Active && r.UserId == user.Id)
                .OrderByDescending(r => r.CreatedOn)
                .Take(5)
                .ToListAsync();
        }
    }
}
