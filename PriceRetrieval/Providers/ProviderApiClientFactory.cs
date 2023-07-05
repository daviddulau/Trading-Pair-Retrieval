using Microsoft.Extensions.DependencyInjection;
using PriceRetrieval.Providers.Bitfinex;
using PriceRetrieval.Providers.Bitstamp;

namespace PriceRetrieval.Providers
{
    public class ProviderApiClientFactory : IProviderApiClientFactory
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly Dictionary<string, Type> _providerTypeDictionary = new(StringComparer.InvariantCultureIgnoreCase)
        {
            { BitfinexApiClient.ProviderName, typeof(BitfinexApiClient) },
            { BitstampApiClient.ProviderName, typeof(BitstampApiClient) }
        };

        public ProviderApiClientFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IProviderApiClient CreateProvider(string providerName)
        {
            if (!_providerTypeDictionary.ContainsKey(providerName))
                throw new ArgumentException($"'{providerName}' is not a valid providerType for method {nameof(CreateProvider)}.");

            var providerType = _providerTypeDictionary[providerName];
            return (IProviderApiClient)_serviceProvider.GetRequiredService(providerType);
        }
    }
}
