using MyRecipeBook.Domain.Dtos;
using MyRecipeBook.Domain.Services.OpenaAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Infrastructure.Services.OpenAI
{
    public class ChatGPTService : IGenerateRecipeAI
    {
        private const string CHAT_MODEL = "gpt-4o";
        public Task<GenerateRecipeDto> Generate(IList<string> ingredients)
        {
            throw new NotImplementedException();
        }
    }
}
