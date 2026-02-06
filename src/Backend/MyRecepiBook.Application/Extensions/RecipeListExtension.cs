using AutoMapper;
using MyRecipeBook.Communication.Response;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Services.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.Extensions
{
    public static class RecipeListExtension
    {
        public static async Task<IList<ResponseShortRecipeJson>> MapToShortRecipeJson(this IList<Recipe> recipes, User user,IBlobStorageService blobStorageService, IMapper mapper)
        {
            var result = recipes.Select(async recipe =>
            {
                var response = mapper.Map<ResponseShortRecipeJson>(recipe);

                if (recipe.ImageIdentifier.Length > 0)
                {
                    response.ImageUrl = await blobStorageService.GetFileUrl(user, recipe.ImageIdentifier);
                }

                return response;
            });

            var response = await Task.WhenAll(result);

            return response;
        }
    }
}