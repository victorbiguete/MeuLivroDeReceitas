using Bogus;
using Microsoft.AspNetCore.Http;
using MyRecipeBook.Communication.Enum;
using MyRecipeBook.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommomTestsUtilities.Requests
{
    public class RequestRegisterRecipeFormDataBuilder
    {
        public static RequestRegisterRecipeFormData Build(IFormFile? formFile =null)
        {
            var step = 1;
            return new Faker<RequestRegisterRecipeFormData>()
                .RuleFor(r => r.Image, _=> formFile)
                .RuleFor(r => r.Title, f => f.Lorem.Word())
                .RuleFor(r => r.CookingTime, f => f.PickRandom<CookingTime>())
                .RuleFor(r => r.Difficulty, f => f.PickRandom<Difficulty>())
                .RuleFor(r => r.Ingredients, f => f.Make(3, () => f.Commerce.ProductName()))
                .RuleFor(r => r.DishTypes, f => f.Make(3, () => f.PickRandom<DishType>()))
                .RuleFor(r => r.Instruction, f => f.Make(3, () => new RequestInstructionJson
                {
                    Text = f.Lorem.Paragraph(),
                    Step = step++,
                }));
        }
    }
}
