using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.UseCases.User.Update
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUserUpdateOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserUseCase(ILoggedUser loggedUser, IUserReadOnlyRepository userReadOnlyRepository, IUserUpdateOnlyRepository repository, IUnitOfWork unitOfWork)
        {
            _loggedUser = loggedUser;
            _userReadOnlyRepository = userReadOnlyRepository;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(RequestUpdateUserJson request)
        {
            var logged = await _loggedUser.User();

            await Validate(request, logged.Email);

            var user = await _repository.GetById(logged.Id);

            user.Name = request.Name;
            user.Email = request.Email;

            _repository.Update(user);

            await _unitOfWork.Commit();
        }

        private async Task Validate(RequestUpdateUserJson request, string currentEmail)
        {
            var validator = new UpdateUserValidator();

            var result = validator.Validate(request);
            

            if(!currentEmail.Equals(request.Email))
            {
                var userExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);

                if(userExist)
                {
                    result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMessagesExceptions.EMAIL_ALREADY_REGISTERED));
                }

                if (!result.IsValid)
                {
                    var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

                    throw new ErrorOnValidationException(errorMessages);
                }
            }
        }
    }
}
