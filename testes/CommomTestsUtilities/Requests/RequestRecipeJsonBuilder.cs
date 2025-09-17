using Bogus;
using MyRecipeBook.Communication.Enum;
using MyRecipeBook.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommomTestsUtilities.Requests
{
    public class RequestRecipeJsonBuilder
    {
        public static RequestRecipeJson Build()
        {
            return new Faker<RequestRecipeJson>()
                .RuleFor(r => r.Title, f => f.Lorem.Word())
                .RuleFor(r => r.CookingTime, f => f.PickRandom<CookingTime>())
                .RuleFor(r => r.Difficulty, f => f.PickRandom<Difficulty>());
        }
    }
}
