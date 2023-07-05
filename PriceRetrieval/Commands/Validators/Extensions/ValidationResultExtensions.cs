using FluentValidation.Results;
using PriceRetrieval.Models.Errors;

namespace PriceRetrieval.Commands.Validators.Extensions
{
    public static class ValidationResultExtensions
    {
        public static IEnumerable<ParameterError> ToParameterErrors(this ValidationResult validationResult)
        {
            return validationResult.Errors
                .Select(e => new ParameterError
                {
                    Name = e.PropertyName ?? string.Empty,
                    Message = e.ErrorMessage,
                    Value = e.AttemptedValue
                });
        }
    }
}
