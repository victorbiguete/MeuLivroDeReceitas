using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Domain.Entities
{
    [Table("Ingredients")]
    public class Ingredient : EntitieBase
    {
        public string Item { get; set; }
        public long RecipeId { get; set; }
    }
}
