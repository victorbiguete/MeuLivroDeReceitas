using MyRecipeBook.Application.UseCases.Recipe.Generate;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Response;
using MyRecipeBook.Domain.Services.OpenAI;
using MyRecipeBook.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.UseCases.Recipe.GenerateRecipe
{
    public class GenerateRecipeUseCase : IGenerateRecipeUseCase
    {
        private readonly IGenerateRecipeAI _generator;

        public GenerateRecipeUseCase(IGenerateRecipeAI generator)
        {
            _generator = generator;
        }

        public async Task<ResponseGenerateRecipeJson> Execute(RequestGenerateRecipeJson request)
        {
            Validate(request);

            var response = await _generator.Generate(request.Ingredients);

            return new ResponseGenerateRecipeJson
            {
                Title = response.Title,
                Ingredients = response.Ingredients,
                CookingTime = (Communication.Enum.CookingTime)response.CookingTime,
                Instructions = response.Instructions.Select(c => new ResponseGeneratedInstructionJson
                {
                    Step = c.Step,
                    Text = c.Text
                }).ToList(),
                Difficulty = Communication.Enum.Difficulty.Low
            };
        }

        private void Validate(RequestGenerateRecipeJson request)
        {
            var result = new GenerateRecipeValidator().Validate(request);

            if (!result.IsValid)
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
        }
    }
}
