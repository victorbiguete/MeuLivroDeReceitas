using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Services.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.UseCases.User.Delete.Delete
{
    public class DeleteUserAccountUseCase : IDeleteUserAccountUseCase
    {
        private readonly IUserDeleteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;

        public DeleteUserAccountUseCase(IUserDeleteOnlyRepository repository, IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;
        }

        public async Task Execute(Guid userIdentifier)
        {
            await _blobStorageService.DeleteContainer(userIdentifier);

            await _repository.DeleteAccount(userIdentifier);

            await _unitOfWork.Commit();
        }
    }
}
