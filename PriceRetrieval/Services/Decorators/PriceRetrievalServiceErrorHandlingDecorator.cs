using PriceRetrieval.Models;
using PriceRetrieval.Utilities.Extensions;

namespace PriceRetrieval.Services.Decorators
{
    public class PriceRetrievalServiceErrorHandlingDecorator : IPriceRetrievalService
    {
        private readonly IPriceRetrievalService _inner;

        public PriceRetrievalServiceErrorHandlingDecorator(
            IPriceRetrievalService inner)
        {
            _inner = inner;
        }

        public async Task<PriceRetrievalResponse> GetPairPrice(PriceRetrievalRequest request)
        {
            try
            {
                return await _inner.GetPairPrice(request);
            }
            catch (Exception ex)
            {
                return request.ToErrorResponse();
            }
        }
    }
}
