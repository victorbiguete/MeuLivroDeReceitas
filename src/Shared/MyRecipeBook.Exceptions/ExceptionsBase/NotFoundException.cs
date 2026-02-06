using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Exceptions.ExceptionsBase
{
    public class NotFoundException : MyRecipeBookExceptions
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
