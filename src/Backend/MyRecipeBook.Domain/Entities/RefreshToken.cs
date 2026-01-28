using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Domain.Entities
{
    public class RefreshToken:EntitieBase
    {
        public required string Value { get; set; } = string.Empty;
        public required long UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
