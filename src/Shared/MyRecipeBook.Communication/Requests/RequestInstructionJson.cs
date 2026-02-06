using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Communication.Requests
{
    public record RequestInstructionJson
    {
        public int Step { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
