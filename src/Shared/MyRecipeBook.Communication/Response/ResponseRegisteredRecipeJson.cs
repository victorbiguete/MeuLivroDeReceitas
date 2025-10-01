using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Communication.Response
{
    public record ResponseRegisteredRecipeJson
    {
        public string Id { get; set; }
        public string Title { get; set; }
    }
}
