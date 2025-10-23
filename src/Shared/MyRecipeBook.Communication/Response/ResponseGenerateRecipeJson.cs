using MyRecipeBook.Communication.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Communication.Response
{
    public class ResponseGenerateRecipeJson
    {
        public string Title { get; set; } = string.Empty;
        public IList<string> Ingredients { get; set; } = [];
        public IList<ResponseGeneratedInstructionJson> Instructions { get; set; } = [];
        public CookingTime CookingTime { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
