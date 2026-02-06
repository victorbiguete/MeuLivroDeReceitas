using CommomTestsUtilities.Dto;
using CommomTestsUtilities.OpenAI;
using CommomTestsUtilities.Requests;
using FluentAssertions;
using MyRecipeBook.Application.UseCases.Recipe.GenerateRecipe;
using MyRecipeBook.Domain.Dtos;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCases.Test.Recipe.Generate
{
    public class GenerateRecipeUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var dto = GeneratedRecipeDtoBuilder.Build();

            var request = RequestGenerateRecipeJsonBuilder.Build();

            var useCase = CreateUseCase(dto);

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Title.Should().Be(dto.Title);
            result.CookingTime.Should().Be((MyRecipeBook.Communication.Enum.CookingTime)dto.CookingTime);
            result.Difficulty.Should().Be(MyRecipeBook.Communication.Enum.Difficulty.Low);
        }

        [Fact]
        public async Task Error_Duplicated_Ingredients()
        {
            var dto = GeneratedRecipeDtoBuilder.Build();

            var request = RequestGenerateRecipeJsonBuilder.Build(count: 4);
            request.Ingredients.Add(request.Ingredients[0]);

            var useCase = CreateUseCase(dto);

            var act = async () => await useCase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorsMessages.Count == 1 &&
                    e.ErrorsMessages.Contains(ResourceMessagesExceptions.DUPLICATED_INGREDIENTS_IN_LIST));
        }

        private static GenerateRecipeUseCase CreateUseCase(GenerateRecipeDto dto)
        {
            var generateRecipeAI = GenerateRecipeAIBuilder.Build(dto);

            return new GenerateRecipeUseCase(generateRecipeAI);
        }
    }
}
