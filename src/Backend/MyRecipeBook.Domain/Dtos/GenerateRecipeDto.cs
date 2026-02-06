using MyRecipeBook.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Domain.Dtos
{
    public record GenerateRecipeDto
    {
        public string Title { get; set; } = string.Empty;
        public IList<string> Ingredients { get; set; } = [];
        public IList<GeneratedInstructionDto> Instructions { get; set; } = [];
        public CookingTime CookingTime { get; set; }
    }
}
