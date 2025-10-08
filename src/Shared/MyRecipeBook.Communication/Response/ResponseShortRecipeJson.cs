using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Communication.Response
{
    public record ResponseShortRecipeJson
    {
        public string Id { get; set; } = string.Empty;
        public string Title {  get; set; } = string.Empty;
        public int AmountIngredients { get; set; }
    }
}
