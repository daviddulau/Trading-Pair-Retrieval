namespace PriceRetrieval.Models
{
    public class PriceRetrievalResponse
    {
        public PriceRetrievalRequest request { get; set; } = new();
        public double ClosePrice { get; set; }
        public PriceRetrievalResult Result { get; set; } = new();
    }
}
