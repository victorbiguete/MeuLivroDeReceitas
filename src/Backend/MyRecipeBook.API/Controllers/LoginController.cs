using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.UseCases.Login.DoLogin;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Response;
using System.Security.Claims;

namespace MyRecipeBook.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : MyRecipeBookBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase, [FromBody] RequestLoginJson request)
        {
            var response = await useCase.Execute(request);

            return Ok(response);
        }

        [HttpGet]
        [Route("google")]
        public async Task<IActionResult> LoginGoogle(string returnUrl)
        {
            var authenticate = await Request.HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            if (IsNoAuthenticated(authenticate))
            {
                return Challenge(GoogleDefaults.AuthenticationScheme);
            }
            else
            {
                var clains = authenticate.Principal!.Identities.First().Claims;

                var name = clains.First(c => c.Type == ClaimTypes.Name).Value;
                
                var email = clains.First(c => c.Type == ClaimTypes.Email).Value;

                return Redirect(returnUrl);
            }
        }
    }
}
