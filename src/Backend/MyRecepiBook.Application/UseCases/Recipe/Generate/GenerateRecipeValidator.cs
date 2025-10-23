using FluentValidation;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.ValueObjects;
using MyRecipeBook.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.UseCases.Recipe.Generate
{
    public class GenerateRecipeValidator:AbstractValidator<RequestGenerateRecipeJson>
    {
        public GenerateRecipeValidator()
        {
            var maximum_number_ingredients = MyRecipeBookRuleConstants.MAXIMUM_INGREDIENTS_GENERATE_RECIPE;

            RuleFor(request => request.Ingredients.Count).InclusiveBetween(1, maximum_number_ingredients).WithMessage(ResourceMessagesExceptions.INVALID_NUMBER_INGREDIENTS);

            RuleFor(request => request.Ingredients).Must(ingredients => ingredients.Count == ingredients.Distinct().Count()).WithMessage(ResourceMessagesExceptions.DUPLICATED_INGREDIENTS_IN_LIST);

            RuleFor(request => request.Ingredients).ForEach(rule =>
            {
                rule.Custom((value, context) =>
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        context.AddFailure(string.Empty, ResourceMessagesExceptions.INGREDIENT_EMPTY);
                        return;
                    }
                    if(value.Count(c => c == ' ') > 3 || value.Count(c => c == '/') > 1)
                    {
                        context.AddFailure(string.Empty, ResourceMessagesExceptions.INGREDIENT_NOT_FOLLOWING_PATTERN);
                    }
                });
                
            });
        }
    }
}
