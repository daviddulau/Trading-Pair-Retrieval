using PriceRetrieval.Commands.Implementations.GetPairPriceCommand;
using PriceRetrieval.Commands.Validators;
using PriceRetrieval.Models;
using PriceRetrieval.Utilities.Extensions;

namespace PriceRetrieval.Services
{
    public class PriceRetrievalService : IPriceRetrievalService
    {
        private readonly PriceRetrievalValidator _priceRetrievalValidator;
        private readonly IGetPairPriceCommand _getPairPriceCommand;

        public PriceRetrievalService(
            PriceRetrievalValidator priceRetrievalValidator,
            IGetPairPriceCommand getPairPriceCommand)
        {
            _priceRetrievalValidator = priceRetrievalValidator;
            _getPairPriceCommand = getPairPriceCommand;
        }

        public async Task<PriceRetrievalResponse> GetPairPrice(PriceRetrievalRequest request)
        {
            var validationResult = await _priceRetrievalValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return request.ToInvalidResponse(validationResult);

            return await _getPairPriceCommand.Execute(request);
        }
    }
}
