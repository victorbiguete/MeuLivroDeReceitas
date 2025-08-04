using CommomTestsUtilities.Cryptography;
using CommomTestsUtilities.Mapper;
using CommomTestsUtilities.Requests;
using CommomTestsUtilities.Repositories;
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
            var mapper = MapperBuilder.Build();

            var passwordEncripter = PasswordEncripterBuilder.Build();

            var writeRepository = UserWriteOnlyRepositoryBuilder.Build();

            var unitOfWork = UnitOfWorkBuilder.Build();

            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = new RegisterUserUseCase();

            var result = await useCase.Execute(request);

            result.Name.Should().NotBeNull();
            result.Name.Should().Be(request.Name);

            
        }
    }
}
