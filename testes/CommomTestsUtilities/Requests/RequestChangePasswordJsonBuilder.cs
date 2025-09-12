using Bogus;
using MyRecipeBook.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommomTestsUtilities.Requests
{
    public class RequestChangePasswordJsonBuilder
    {
        public static RequestChangePasswordJson Build(int passwordLegth = 10)
        {
            return new Faker<RequestChangePasswordJson>()
                .RuleFor(u => u.Password, (f) => f.Internet.Password())
                .RuleFor(u => u.NewPassword, (f) => f.Internet.Password(passwordLegth));
        }
    }
}
