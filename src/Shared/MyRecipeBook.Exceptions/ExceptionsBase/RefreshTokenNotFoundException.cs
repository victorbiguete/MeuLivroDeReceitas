using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Exceptions.ExceptionsBase
{
    public class RefreshTokenNotFoundException : MyRecipeBookExceptions
    {
        public RefreshTokenNotFoundException() : base(ResourceMessagesExceptions.EXPIRED_SESSION)
        {
        }
        
    }
}
