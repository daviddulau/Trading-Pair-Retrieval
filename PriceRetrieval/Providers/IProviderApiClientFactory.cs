namespace PriceRetrieval.Providers
{
    public interface IProviderApiClientFactory
    {
        IProviderApiClient CreateProvider(string providerName);
    }
}
