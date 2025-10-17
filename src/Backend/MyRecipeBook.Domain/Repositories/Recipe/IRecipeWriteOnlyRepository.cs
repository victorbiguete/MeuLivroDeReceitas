using MyRecipeBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Domain.Repositories.Recipe
{
    public interface IRecipeWriteOnlyRepository
    {
        public Task Add(Entities.Recipe recipe);
        public Task Delete(long id);
    }
}
