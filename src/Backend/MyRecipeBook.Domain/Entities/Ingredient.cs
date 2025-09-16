using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Domain.Entities
{
    public class Ingredient : EntitieBase
    {
        public string Item { get; set; }
        public long RecipeId { get; set; }
    }
}
