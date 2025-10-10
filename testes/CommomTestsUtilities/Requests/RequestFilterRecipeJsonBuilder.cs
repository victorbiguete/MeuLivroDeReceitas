using Bogus;
using MyRecipeBook.Communication.Enum;
using MyRecipeBook.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CommomTestsUtilities.Requests
{
    public class RequestFilterRecipeJsonBuilder
    {
        public static RequestFilterRecipeJson Build()
        {
            return new Faker<RequestFilterRecipeJson>()
                .RuleFor(user => user.CookingTime, faker => faker.Make(1,()=> faker.PickRandom<CookingTime>()))
                .RuleFor(user => user.Difficulties, faker => faker.Make(1,()=> faker.PickRandom<Difficulty>()))
                .RuleFor(user => user.DishTypes, faker => faker.Make(1,()=> faker.PickRandom<DishType>()))
                .RuleFor(user => user.RecipeTitle_Ingredient, faker => faker.Lorem.Word());
        }
    }
}
