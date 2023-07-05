using PriceRetrieval.Models;

namespace PriceRetrieval.Commands.Implementations.GetPairPriceCommand
{
    public interface IGetPairPriceCommand
    {
        Task<PriceRetrievalResponse> Execute(PriceRetrievalRequest request);
    }
}
