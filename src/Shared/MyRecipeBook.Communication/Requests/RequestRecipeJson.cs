using MyRecipeBook.Communication.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Communication.Requests
{
    public class RequestRecipeJson
    {
        public string Title { get; set; } = string.Empty;
        public CookingTime? CookingTime { get; set; }
        public Difficulty? Difficulty { get; set; }
        public IList<string> Ingredients { get; set; } = [];
        public IList<RequestInstructionJson> Instruction { get; set; } = [];
        public IList<DishType> DishTypes { get; set; } = [];
    }
}
