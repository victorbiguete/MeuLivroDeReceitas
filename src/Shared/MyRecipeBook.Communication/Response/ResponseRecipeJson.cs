using MyRecipeBook.Communication.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Communication.Response
{
    public class ResponseRecipeJson
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public IList<ResponseIngredientJson> Ingredients { get; set; } = [];
        public IList<ResponseInstructionJson> Instructions { get; set; } = [];
        public IList<DishType> DishTypes { get; set; } = [];
        public CookingTime? CookingTime { get; set; }
        public Difficulty? Difficulty { get; set; }
    }
}
