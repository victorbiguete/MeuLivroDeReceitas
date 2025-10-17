using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.UseCases.Recipe.Delete
{
    public interface IDeleteRecipeUseCase
    {
        public Task Execute(long id);
    }
}
