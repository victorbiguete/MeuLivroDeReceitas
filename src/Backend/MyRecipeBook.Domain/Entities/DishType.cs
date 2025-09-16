using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Domain.Entities
{
    public class DishType : EntitieBase
    {
        public Enum.DishType Type { get; set; }
        public long RecipeId { get; set; }
    }
}
