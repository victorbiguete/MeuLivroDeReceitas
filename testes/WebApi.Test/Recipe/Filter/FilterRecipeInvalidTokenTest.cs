using CommomTestsUtilities.Requests;
using CommomTestsUtilities.Tokens;
using FluentAssertions;
using System.Net;

namespace WebApi.Test.Recipe.Filter
{
    public class FilterRecipeInvalidTokenTest : MyRecipeBookClassFixture
    {
        private const string METHOD = "recipe/filter";
        
        public FilterRecipeInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Error_Token_Invalid()
        {
            var request = RequestFilterRecipeJsonBuilder.Build();
            var response = await DoPost(method:METHOD, request: request,token:"TokenIsInvalid");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var request = RequestFilterRecipeJsonBuilder.Build();
            var response = await DoPost(method:METHOD,request:request,token:string.Empty);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Token_With_User_NotFound()
        {
            var request = RequestFilterRecipeJsonBuilder.Build();
            var token = JwtTokenGeneratorBuilder.Build().Generate(new Guid());
            var response = await DoPost(method:METHOD, request: request,token:token);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
