using CommomTestsUtilities.IdEncryption;
using CommomTestsUtilities.Tokens;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Test.Recipe.GetById
{
    public class GetRecipeByIdInvalidTokenTest : MyRecipeBookClassFixture
    {
        private readonly string METHOD = "recipe";
        public GetRecipeByIdInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Error_Token_Invalid()
        {
            var id = IdEncripterBuilder.Build().Encode(1);

            var response = await DoGet(method: $"{METHOD}/{id}", token: "tokenInvalid");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Token_Empty()
        {
            var id = IdEncripterBuilder.Build().Encode(1);

            var response = await DoGet(method: $"{METHOD}/{id}", token: string.Empty);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Token_With_User_NotFound()
        {
            var id = IdEncripterBuilder.Build().Encode(1);

            var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());

            var response = await DoGet(method: $"{METHOD}/{id}", token: token);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }
    }
}
