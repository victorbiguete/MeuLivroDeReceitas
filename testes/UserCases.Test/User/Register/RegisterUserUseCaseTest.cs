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
        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase();

            var result = await useCase.Execute(request);

            result.Name.Should().NotBeNull();
            result.Name.Should().Be(request.Name);

        }

        public async Task Error_Email_Already_Registered()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase(request.Email);
        }

        private RegisterUserUseCase CreateUseCase(string? email = null)
        {
            var mapper = MapperBuilder.Build();

            var passwordEncripter = PasswordEncripterBuilder.Build();

            var writeRepository = UserWriteOnlyRepositoryBuilder.Build();

            var unitOfWork = UnitOfWorkBuilder.Build();

            var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder();

            if(!string.IsNullOrEmpty(email))
                readRepositoryBuilder.ExistActiveUserWithEmail(email);

            return new RegisterUserUseCase(readRepositoryBuilder.Build(), writeRepository, mapper, passwordEncripter, unitOfWork);
        }
    }
}
