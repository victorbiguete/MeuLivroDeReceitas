using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CommomTestsUtilities.Requests;
using CommomTestsUtilities.Tokens;
using FluentAssertions;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Enum;
using WebApi.Test.InlineData;

namespace WebApi.Test.Recipe.Filter
{
    public class FilterRecipeTest : MyRecipeBookClassFixture
    {
        private const string METHOD = "recipe/filter";

        private readonly Guid _userIdentifier;

        private string _recipeTitle;
        private Difficulty _difficulty;
        private CookingTime _cookingTime;
        private IList<DishType> _dishTypes;

        public FilterRecipeTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _userIdentifier = factory.GetUserIdentifier();
            _cookingTime = factory.GetRecipeCookingTime();
            _difficulty = factory.GetRecipeDifficulty();
            _dishTypes = factory.GetDishType();
            _recipeTitle = factory.GetRecipeTitle();
        }

        [Fact]
        public async Task Success()
        {
            var request = new RequestFilterRecipeJson
            {
                CookingTime = [(MyRecipeBook.Communication.Enum.CookingTime)_cookingTime],
                
                Difficulties = [(MyRecipeBook.Communication.Enum.Difficulty)_difficulty],
                
                DishTypes = _dishTypes.Select(dishType => (MyRecipeBook.Communication.Enum.DishType)dishType).ToList(),
                
                RecipeTitle_Ingredient = _recipeTitle,
            };

            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

            var response = await DoPost(method:METHOD, request:request, token:token);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.GetProperty("recipes").EnumerateArray().Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Success_NoContent()
        {
            var request = RequestFilterRecipeJsonBuilder.Build();
            request.RecipeTitle_Ingredient = "recipeDontExist";

            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

            var response = await DoPost(METHOD, request: request, token: token);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_CookingTime_Invalid(string culture)
        {
            var request = RequestFilterRecipeJsonBuilder.Build();
            request.CookingTime.Add((MyRecipeBook.Communication.Enum.CookingTime)1000);

            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

            var response = await DoPost(METHOD, request: request, token: token);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
