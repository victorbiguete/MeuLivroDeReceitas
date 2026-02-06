using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Communication.Requests
{
    public record RequestLoginJson
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty ;
    }
}
