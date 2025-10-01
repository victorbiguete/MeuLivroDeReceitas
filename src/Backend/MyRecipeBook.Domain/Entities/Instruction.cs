using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Domain.Entities
{
    [Table("Instructions")]
    public class Instruction : EntitieBase
    {
        public int Step { get; set; }
        public string Text { get; set; } = string.Empty;
        [Column("RecipesId")]
        public long RecipeId { get; set; }
    }
}
