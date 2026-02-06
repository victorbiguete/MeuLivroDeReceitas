using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Domain.Services.ServiceBus;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.UseCases.User.Delete.Request
{
    public class RequestDeleteUserUseCase : IRequestDeleteUserUseCase
    {
        private readonly IDeleteUserQueue _queue;
        private readonly IUserUpdateOnlyRepository _repository;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;

        public RequestDeleteUserUseCase(IDeleteUserQueue queue, IUserUpdateOnlyRepository repository, ILoggedUser loggedUser, IUnitOfWork unitOfWork)
        {
            _queue = queue;
            _repository = repository;
            _loggedUser = loggedUser;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute()
        {
            var loggedUser = await _loggedUser.User();

            var user = await _repository.GetById(loggedUser.Id);

            user.Active = false;
            _repository.Update(user);

            await _unitOfWork.Commit();
            await _queue.SendMessage(loggedUser);
        }
    }
}
