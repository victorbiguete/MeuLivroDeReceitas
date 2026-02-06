using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Exceptions.ExceptionsBase
{
    public class MyRecipeBookExceptions : Exception
    {
        public MyRecipeBookExceptions(string message) : base(message) 
        {

        }
    }
}
