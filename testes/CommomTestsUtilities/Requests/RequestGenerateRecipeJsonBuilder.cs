using Bogus;
using MyRecipeBook.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommomTestsUtilities.Requests
{
    public class RequestGenerateRecipeJsonBuilder
    {
        public static RequestGenerateRecipeJson Build(int count = 5)
        {
            return new Faker<RequestGenerateRecipeJson>()
                .RuleFor(user => user.Ingredients, f => f.Make(count, () => f.Commerce.ProductName()));
        }
    }
}
