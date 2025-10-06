using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CommomTestsUtilities.IdEncryption
{
    public class IdEncripterBuilder
    {
       public static SqidsEncoder<long> Build()
        {
            return new SqidsEncoder<long>(new()
            {
                MinLength = 3,
                Alphabet = "0ULsQ3PdIckhG4SlHNtMeE5nqz2xf7RXDOprbvKgAuVio1FmCaB6JTj8Z9"
            });
        }
    }
}
