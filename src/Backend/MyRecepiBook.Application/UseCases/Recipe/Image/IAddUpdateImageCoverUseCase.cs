using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.UseCases.Recipe.Image
{
    public interface IAddUpdateImageCoverUseCase
    {
        public Task Execute(long recipeId, IFormFile file);
    }
}
