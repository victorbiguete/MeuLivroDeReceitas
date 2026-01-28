using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Communication.Requests
{
    public class RequestNewTokenJson
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
