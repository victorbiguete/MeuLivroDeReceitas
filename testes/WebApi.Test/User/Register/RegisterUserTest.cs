using CommomTestsUtilities.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace WebApi.Test.User.Register
{
    public class RegisterUserTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        public RegisterUserTest(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            await _httpClient.PostAsJsonAsync("User",request);
        }
    }
}
