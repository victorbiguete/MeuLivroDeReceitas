using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Communication.Response
{
    public class ResponseInstructionJson
    {
        public string Id { get; set; } = string.Empty;
        public int Step { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
