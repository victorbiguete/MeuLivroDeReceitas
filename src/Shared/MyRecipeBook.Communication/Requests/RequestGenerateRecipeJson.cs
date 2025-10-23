using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Communication.Requests
{
    public class RequestGenerateRecipeJson
    {
        public IList<string> Ingredients { get; set; } = [];
    }
}
