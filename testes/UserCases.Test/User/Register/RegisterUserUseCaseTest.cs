using CommomTestsUtilities.Cryptography;
using CommomTestsUtilities.Mapper;
using CommomTestsUtilities.Requests;
using FluentAssertions;
using MyRecipeBook.Application.UseCases.User.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCases.Test.User.Register
{
    public class RegisterUserUseCaseTest
    {
        public async Task Success()
        {
            var useCase = new RegisterUserUseCase();

            var mapper = MapperBuilder.Build();

            var passwordEncripter = PasswordEncripterBuilder.Build();

            var request = RequestRegisterUserJsonBuilder.Build();

            var result = await useCase.Execute(request);

            result.Name.Should().NotBeNull();
            result.Name.Should().Be(request.Name);

            
        }
    }
}
