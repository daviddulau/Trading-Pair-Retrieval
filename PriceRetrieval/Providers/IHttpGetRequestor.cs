namespace PriceRetrieval.Providers
{
    public interface IHttpGetRequestor
    {
        Task<Result<double>> GetPairPrice(string requestUri, Func<string, string> responseProcessor);
    }
}
