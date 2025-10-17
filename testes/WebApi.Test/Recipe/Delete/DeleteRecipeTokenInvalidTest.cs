using CommomTestsUtilities.IdEncryption;
using CommomTestsUtilities.Tokens;
using FluentAssertions;
using MyRecipeBook.Infrastructure.Security.Tokens.Access.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Test.Recipe.Delete
{
    public class DeleteRecipeTokenInvalidTest:MyRecipeBookClassFixture
    {
        private const string METHOD = "recipe";

        public DeleteRecipeTokenInvalidTest(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Error_Token_Invalid()
        {
            var id = IdEncripterBuilder.Build().Encode(1);
            var response = await DoDelete(method: $"{METHOD}/{id}", token:"isInvalid");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }
        [Fact]
        public async Task Error_Token_Empty()
        {
            var id = IdEncripterBuilder.Build().Encode(1);
            var response = await DoDelete(method: $"{METHOD}/{id}", token:string.Empty);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Token_Without_User()
        {
            var id = IdEncripterBuilder.Build().Encode(1);
            var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());
            var response = await DoDelete(method: $"{METHOD}/{id}", token:token);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }


    }
}
