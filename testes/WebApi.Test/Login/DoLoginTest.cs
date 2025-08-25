using CommomTestsUtilities.Requests;
using FluentAssertions;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.Test.InlineData;

namespace WebApi.Test.Login
{
    public class DoLoginTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly string method = "login";
        private readonly HttpClient _httpClient;
        private readonly string _email;
        private readonly string _password;
        private readonly string _name;

        public DoLoginTest(CustomWebApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
            _email = factory.GetEmail();
            _password = factory.GetPassword();
            _name = factory.GetName();
        }

        [Fact]
        public async Task Success()
        {
            var request = new RequestLoginJson
            {
                Email = _email,
                Password = _password
            };

            var response = await _httpClient.PostAsJsonAsync(method, request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.GetProperty("name")
                .GetString()
                .Should()
                .NotBeNullOrWhiteSpace()
                .And.Be(_name);
        }
        

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Login_Invalid(string culture)
        {
            var request = RequestLoginJsonBuilder.Build();

            if (_httpClient.DefaultRequestHeaders.Contains("Accept-Language"))
                _httpClient.DefaultRequestHeaders.Remove("Accept-Language");

            _httpClient.DefaultRequestHeaders.Add("Accept-Language", culture);

            var response = await _httpClient.PostAsJsonAsync(method, request);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

            var expectedMessage = ResourceMessagesExceptions.ResourceManager.GetString("EMAIL_OR_PASSWORD_INVALID", new CultureInfo(culture));

            errors.Should()
                .ContainSingle()
                .And.Contain(error => error.GetString()!.Equals(expectedMessage));
        }
    }
}
