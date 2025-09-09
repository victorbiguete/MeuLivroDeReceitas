using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Communication.Response
{
    public record ResponseUserProfileJson
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
