using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Communication.Response
{
    public class ResponseUserJson
    {
        public IList<string> Errors { get; set; }

        public ResponseUserJson(IList<string> errors)
        {
            Errors = errors;
        }

        public ResponseUserJson(string error)
        {
            //Errors = [error];
            Errors = new List<string>
            {
                error
            };
        }
    }
}
