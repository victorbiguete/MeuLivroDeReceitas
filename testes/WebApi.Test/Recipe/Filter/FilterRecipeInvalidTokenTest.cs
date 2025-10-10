using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Test.Recipe.Filter
{
    public class FilterRecipeInvalidTokenTest : MyRecipeBookClassFixture
    {
        private const string METHOD = "recipe/filter";

        public FilterRecipeInvalidTokenTest(CustomWebApplicationFactory webApplication) : base(webApplication)
        {
        }

        [Fact]
        public async Task Error_Token_Invalid()
        {

        }

        [Fact]
        public async Task Error_Without_Token()
        {

        }

        [Fact]
        public async Task Error_Token_With_User_NotFound()
        {

        }
    }
}
