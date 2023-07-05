using FluentValidation;

namespace PriceRetrieval.Commands.Validators.Extensions
{
    public static class CustomValidationExtensions
    {
        public static IRuleBuilderOptions<T, string?> MustBeValidDate<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder.Matches(Constants.DateTimeRegex)
                .WithMessage("Must match yyyy-MM-ddTHH:mm:ss");
        }

        public static IRuleBuilderOptions<T, string?> MustBeValidPair<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder.Matches(Constants.PairRegex)
                .WithMessage("Must match XXXXXX");
        }
    }
}
