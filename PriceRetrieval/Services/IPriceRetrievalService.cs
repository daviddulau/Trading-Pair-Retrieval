using PriceRetrieval.Models;

namespace PriceRetrieval.Services
{
    public interface IPriceRetrievalService
    {
        public Task<PriceRetrievalResponse> GetPairPrice(PriceRetrievalRequest request);
    }
}