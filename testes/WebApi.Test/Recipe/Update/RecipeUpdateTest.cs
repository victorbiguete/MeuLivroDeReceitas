using CommomTestsUtilities.Requests;
using CommomTestsUtilities.Tokens;
using FluentAssertions;
using MyRecipeBook.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.Test.InlineData;

namespace WebApi.Test.Recipe.Update
{
    public class RecipeUpdateTest:MyRecipeBookClassFixture
    {
        private const string METHOD = "recipe";

        private readonly Guid _userIdentifier;
        private readonly string _recipeId;

        public RecipeUpdateTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _userIdentifier = factory.GetUserIdentifier();
            _recipeId = factory.GetRecipeId();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestRecipeJsonBuilder.Build();
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

            var response = await DoPut($"{METHOD}/{_recipeId}",request,token);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Title_Empty(string culture)
        {
            var request = RequestRecipeJsonBuilder.Build();
            request.Title = string.Empty;
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

            var response = await DoPut($"{METHOD}/{_recipeId}", request, token,culture);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

            await using var responseBody =  await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

            var expectedMessage = ResourceMessagesExceptions.ResourceManager.GetString("RECIPE_TITLE_EMPTY", new System.Globalization.CultureInfo(culture));

            errors.Should().HaveCount(1).And.Contain(c => c.GetString()!.Equals(expectedMessage));
        }
    }
}
