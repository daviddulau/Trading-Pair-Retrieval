using FluentValidation;
using PriceRetrieval.Commands.Validators.Extensions;
using PriceRetrieval.Models;

namespace PriceRetrieval.Commands.Validators
{
    public class PriceRetrievalValidator : AbstractValidator<PriceRetrievalRequest>
    {
        public PriceRetrievalValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(c => c.Pair)
                .NotEmpty()
                .MustBeValidPair();

            RuleFor(c => c.DateTime)
                .NotEmpty()
                .MustBeValidDate();
        }
    }
}
