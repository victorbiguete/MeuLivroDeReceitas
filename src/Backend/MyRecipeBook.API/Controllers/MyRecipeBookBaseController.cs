using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyRecipeBook.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MyRecipeBookBaseController : ControllerBase
    {
        protected static bool IsNoAuthenticated(AuthenticateResult authenticate)
        {
            return !authenticate.Succeeded || authenticate.Principal is null ||
                !authenticate.Principal.Identities.Any(id => id.IsAuthenticated);
        }
    }
}
