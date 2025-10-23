using MyRecipeBook.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Domain.Services.OpenaAI
{
    public interface IGenerateRecipeAI
    {
        Task<GenerateRecipeDto> Generate(IList<string> ingredients);
    }
}
