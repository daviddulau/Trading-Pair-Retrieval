using PriceRetrieval.Models;

namespace PriceRetrieval.Providers
{
    public interface IProviderApiClient
    {
        static string? ProviderName { get; }
        Task<Result<double>> GetPairPrice(PriceRetrievalRequest request);
    }
}