using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validator.Test.User.Register
{
    public class RegisterUserValidatorTest
    {
        [Fact]
        public void Sucess()
        {
            var validator = new RegisterUserValidator();

            

            var result = validator.Validate();

            result.IsValid == true
        }
    }
}
