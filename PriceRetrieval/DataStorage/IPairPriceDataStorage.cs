using PriceRetrieval.Models;

namespace PriceRetrieval.DataStorage
{
    public interface IPairPriceDataStorage
    {
        void CreateTableCommand();
        void InsertPairCommand(PriceRetrievalRequest request, double closePrice);
        string GetPair(PriceRetrievalRequest request);
    }
}
