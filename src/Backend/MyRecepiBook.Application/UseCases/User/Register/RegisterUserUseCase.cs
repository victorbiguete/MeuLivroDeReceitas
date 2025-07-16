using MyRecipeBook.Application.Cryptography;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Response;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions.ExceptionsBase;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        private readonly IUserReadOnlyRepository _readOnlyRepository;
        private readonly IUserWriteOnlyRepository _writeOnlyRepository;

        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
        {
            var criptografiaDeSenha = new PasswordEncripter();

            var autoMapper = new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new Services.AutoMapper.AutoMapping());
            }).CreateMapper();

            Validate(request);

            var user = autoMapper.Map<Domain.Entities.User>(request);

            user.Password = criptografiaDeSenha.Encrypt(request.Password);

            await _writeOnlyRepository.Add(user);

            return new ResponseRegisteredUserJson
            {
                Name = request.Name
            };
        }
        private void Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
