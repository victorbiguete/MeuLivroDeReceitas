using CommomTestsUtilities.Requests;
using FluentAssertions;
using MyRecipeBook.Application.UseCases.Recipe;
using MyRecipeBook.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCases.Test.Recipe
{
    public class RecipeValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new RecipeValidator();

            var request = RequestRecipeJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_Invalid_Cooking_Time()
        {
            var validator = new RecipeValidator();

            var request = RequestRecipeJsonBuilder.Build();
            request.CookingTime = (MyRecipeBook.Communication.Enum.CookingTime?)1000;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.COOKING_TIME_NOT_SUPPORTED));
        }

        [Fact]
        public void Error_Invalid_Difficulty()
        {
            var validator = new RecipeValidator();

            var request = RequestRecipeJsonBuilder.Build();
            request.Difficulty = (MyRecipeBook.Communication.Enum.Difficulty?)1000;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.DIFFICULTY_LEVEL_NOT_SUPPORTED));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("      ")]
        [InlineData("")]
        public void Error_Empty_Title(string title)
        {
            var validator = new RecipeValidator();

            var request = RequestRecipeJsonBuilder.Build();
            request.Title = title;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.RECIPE_TITLE_EMPTY));
        }

        [Fact]
        public void Success_Cooking_Time_Null()
        {
            var validator = new RecipeValidator();

            var request = RequestRecipeJsonBuilder.Build();
            request.CookingTime = null;

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Success_Difficulty_Null()
        {
            var validator = new RecipeValidator();

            var request = RequestRecipeJsonBuilder.Build();
            request.Difficulty = null;

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }
    }
}
