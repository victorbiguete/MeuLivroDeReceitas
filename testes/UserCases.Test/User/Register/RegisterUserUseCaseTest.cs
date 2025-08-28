using CommomTestsUtilities.Cryptography;
using CommomTestsUtilities.Mapper;
using CommomTestsUtilities.Repositories;
using CommomTestsUtilities.Requests;
using CommomTestsUtilities.Tokens;
using FluentAssertions;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
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
            result.Tokens.Should().NotBeNull();
            result.Name.Should().Be(request.Name);
            result.Tokens.AccessToken.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Error_Email_Already_Registered()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase(request.Email);

            Func<Task> act = async () => await useCase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorsMessages.Count == 1 && e.ErrorsMessages.Contains(ResourceMessagesExceptions.EMAIL_ALREADY_REGISTERED));
        }
        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            request.Name = string.Empty;

            var useCase = CreateUseCase();

            Func<Task> act = async () => await useCase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorsMessages.Count == 1 && e.ErrorsMessages.Contains(ResourceMessagesExceptions.NAME_EMPTY));
        }

        private static RegisterUserUseCase CreateUseCase(string? email = null)
        {
            var mapper = MapperBuilder.Build();

            var passwordEncripter = PasswordEncripterBuilder.Build();

            var writeRepository = UserWriteOnlyRepositoryBuilder.Build();

            var unitOfWork = UnitOfWorkBuilder.Build();

            var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder();

            var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();

            if (!string.IsNullOrEmpty(email))
                readRepositoryBuilder.ExistActiveUserWithEmail(email);

            return new RegisterUserUseCase(readRepositoryBuilder.Build(), writeRepository, mapper, passwordEncripter, unitOfWork, accessTokenGenerator);
        }
    }
}
