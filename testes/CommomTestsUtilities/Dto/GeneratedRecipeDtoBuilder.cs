using Bogus;
using MyRecipeBook.Domain.Dtos;
using MyRecipeBook.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommomTestsUtilities.Dto
{
    public class GeneratedRecipeDtoBuilder
    {
        public static GenerateRecipeDto Build()
        {
            return new Faker<GenerateRecipeDto>()
                .RuleFor(r => r.Title, f => f.Lorem.Word())
                .RuleFor(r => r.CookingTime, f => f.PickRandom<CookingTime>())
                .RuleFor(r => r.Ingredients, f => f.Make(1, () => f.Commerce.ProductName()))
                .RuleFor(r => r.Instructions, f => f.Make(1, () => new GeneratedInstructionDto
                {
                    Step = 1,
                    Text = f.Lorem.Paragraph()
                }));

        } 
    }
}
