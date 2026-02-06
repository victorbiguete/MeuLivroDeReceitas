using MyRecipeBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Domain.Repositories.Recipe
{
    public interface IRecipeUpdateOnlyRepository
    {
        Task<Entities.Recipe?> GetById(Entities.User user, long recipeId);
        void Update(Entities.Recipe recipe);
    }
}
