using CommomTestsUtilities.Entities;
using CommomTestsUtilities.IdEncryption;
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

namespace WebApi.Test.Recipe.Delete
{
    public class DeleteRecipeTest:MyRecipeBookClassFixture
    {
        private const string METHOD = "recipe";

        private readonly Guid _userIdentifier;
        private readonly string _recipeID;

        public DeleteRecipeTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _userIdentifier = factory.GetUserIdentifier();
            _recipeID = factory.GetRecipeId();
        }

        [Fact]
        public async Task Success()
        {
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

            var response = await DoDelete(method: $"{METHOD}/{_recipeID}",token:token);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

            response = await DoGet($"{METHOD}/{_recipeID}", token);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Recipe_Not_Found(string culture)
        {
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

            var id = IdEncripterBuilder.Build().Encode(1000);

            var response = await DoDelete(method: $"{METHOD}/{id}", token: token,culture);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();
            
            var expectedMessage = ResourceMessagesExceptions.ResourceManager.GetString("RECIPE_NOT_FOUND", new System.Globalization.CultureInfo(culture));

            errors.Should().HaveCount(1).And.Contain(c => c.GetString()!.Equals(expectedMessage));
        }
    }
}
