using AutoMapper;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.UseCases.Recipe.Update
{
    public class UpdateRecipeUseCase : IUpdateRecipeUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRecipeUpdateOnlyRepository _repository;
        private readonly IMapper _mapper;

        public UpdateRecipeUseCase(ILoggedUser loggedUser, IUnitOfWork unitOfWork, IRecipeUpdateOnlyRepository repository, IMapper mapper)
        {
            _loggedUser = loggedUser;
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Execute(long recipeId, RequestRecipeJson request)
        {
            Validate(request);

            var loggedUser = await _loggedUser.User();

            var recipe = await _repository.GetById(loggedUser, recipeId);

            if (recipe is null)
                throw new NotFoundException(ResourceMessagesExceptions.RECIPE_NOT_FOUND);

            recipe.Ingredients.Clear();
            recipe.Instructions.Clear();
            recipe.DishTypes.Clear();

            _mapper.Map(request, recipe);

            var instructions = request.Instruction.OrderBy(i => i.Step).ToList();

            for(var index = 0;index < instructions.Count;index++)
            {
                instructions.ElementAt(index).Step = index + 1;
            }

            recipe.Instructions = _mapper.Map<IList<Domain.Entities.Instruction>>(instructions);    

            _repository.Update(recipe);

            await _unitOfWork.Commit();
        }

        private static void Validate(RequestRecipeJson request)
        {
            var result = new RecipeValidator().Validate(request);

            if(!result.IsValid)
            {
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
            }
        }
    }
}
