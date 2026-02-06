using CommomTestsUtilities.Tokens;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApi.Test.Dashboard
{
    public class GetDashboardTest:MyRecipeBookClassFixture
    {
        private const string METHOD = "dashboard";
        private readonly Guid _userIdentifier;

        public GetDashboardTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _userIdentifier = factory.GetUserIdentifier();
        }

        [Fact]
        public async Task Success()
        {
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);
            var response = await DoGet(METHOD, token);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.GetProperty("recipes").GetArrayLength().Should().BeGreaterThan(0);
        }
    }
}
