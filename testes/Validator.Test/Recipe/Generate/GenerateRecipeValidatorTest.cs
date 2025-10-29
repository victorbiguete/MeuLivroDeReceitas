using CommomTestsUtilities.Requests;
using FluentAssertions;
using MyRecipeBook.Application.UseCases.Recipe.Generate;
using MyRecipeBook.Domain.ValueObjects;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validator.Test.Recipe.Generate
{
    public class GenerateRecipeValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new GenerateRecipeValidator();

            var request = RequestGenerateRecipeJsonBuilder.Build();
            
            var result = validator.Validate(request);
            
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_More_Maximum_Ingredient()
        {
            var validator = new GenerateRecipeValidator();

            var request = RequestGenerateRecipeJsonBuilder.Build(count: MyRecipeBookRuleConstants.MAXIMUM_INGREDIENTS_GENERATE_RECIPE + 1);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.INVALID_NUMBER_INGREDIENTS));
        }

        [Fact]
        public void Error_Duplicated_Ingredient()
        {
            var validator = new GenerateRecipeValidator();

            var request = RequestGenerateRecipeJsonBuilder.Build(count: MyRecipeBookRuleConstants.MAXIMUM_INGREDIENTS_GENERATE_RECIPE - 1);
            request.Ingredients.Add(request.Ingredients[0]);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.DUPLICATED_INGREDIENTS_IN_LIST));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("      ")]
        [InlineData("")]
        [SuppressMessage("Usage","xUnit1012:Null should only be used for nullable parameters", Justification = "Because it is a unit test")]
        public void Error_Empty_Ingredient(string ingredient)
        {
            var validator = new GenerateRecipeValidator();

            var request = RequestGenerateRecipeJsonBuilder.Build(count:1);
            request.Ingredients.Add(ingredient);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(errors => errors.ErrorMessage.Equals(ResourceMessagesExceptions.INGREDIENT_EMPTY));
        }

        [Fact]
        public void Error_Ingredient_Not_Following_Pattern()
        {
            var validator = new GenerateRecipeValidator();

            var request = RequestGenerateRecipeJsonBuilder.Build(count: MyRecipeBookRuleConstants.MAXIMUM_INGREDIENTS_GENERATE_RECIPE - 1);
            request.Ingredients.Add("This is an invalid ingredient because is too long");
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(errors => errors.ErrorMessage.Equals(ResourceMessagesExceptions.INGREDIENT_NOT_FOLLOWING_PATTERN));
        }
    }
}
