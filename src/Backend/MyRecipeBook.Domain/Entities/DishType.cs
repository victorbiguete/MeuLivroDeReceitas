using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Domain.Entities
{
    [Table("DishTypes")]
    public class DishType : EntitieBase
    {
        public Enum.DishType Type { get; set; }
        public long RecipeId { get; set; }
    }
}
