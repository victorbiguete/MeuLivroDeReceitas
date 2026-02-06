using AutoMapper;
using Microsoft.Extensions.Options;
using MyRecipeBook.Application.Extensions;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Response;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Domain.Services.Storage;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.UseCases.Recipe
{
    public class RegisterRecipeUseCase : IRegisterRecipeUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IRecipeWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBlobStorageService _blobStorageService;

        public RegisterRecipeUseCase(ILoggedUser loggedUser, IRecipeWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IBlobStorageService blobStorageService)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _blobStorageService = blobStorageService;
        }

        public async Task<ResponseRegisteredRecipeJson> Execute(RequestRegisterRecipeFormData request)
        {
            Validate(request);

            var loggedUser = await _loggedUser.User();

            var recipe = _mapper.Map<Domain.Entities.Recipe>(request);
            recipe.UserId = loggedUser.Id;

            var instructions = request.Instruction.OrderBy(i => i.Step).ToList();
            for(var index = 0; index< instructions.Count; index++)
            {
                instructions[index].Step = index + 1;
            }

            recipe.Instructions = _mapper.Map<IList<Domain.Entities.Instruction>>(instructions);

            if(request.Image is not null)
            {
                var fileStream = request.Image.OpenReadStream();

                (var isValidImage, var extension) = fileStream.ValidateAndGetImageExtension();

                if(!isValidImage)
                {
                    throw new ErrorOnValidationException([ResourceMessagesExceptions.ONLY_IMAGES_ACCEPTED]);
                }

                recipe.ImageIdentifier = $"{Guid.NewGuid()}{extension}";
                await _blobStorageService.Upload(loggedUser,fileStream,recipe.ImageIdentifier);
            }

            await _repository.Add(recipe);
            await _unitOfWork.Commit();

            return _mapper.Map<ResponseRegisteredRecipeJson>(recipe);
        }

        private static void Validate(RequestRecipeJson request)
        {
            var result = new RecipeValidator().Validate(request);

            if (!result.IsValid)
            {
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
            }
        }
    }
}
