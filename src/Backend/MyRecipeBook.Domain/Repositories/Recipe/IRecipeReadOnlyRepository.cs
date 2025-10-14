using MyRecipeBook.Domain.Dtos;
using MyRecipeBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Domain.Repositories.Recipe
{
    public interface IRecipeReadOnlyRepository
    {
        Task<IList<Entities.Recipe>> Filter(Entities.User user, FilterRecipesDto filters);
        Task<Entities.Recipe?> GetById(Entities.User user, long recipeId);
    }
}
