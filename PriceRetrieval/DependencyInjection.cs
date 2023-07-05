using Microsoft.Extensions.DependencyInjection;
using PriceRetrieval.Commands.Implementations.GetPairPriceCommand;
using PriceRetrieval.Commands.Validators;
using PriceRetrieval.DataStorage;
using PriceRetrieval.Parsers;
using PriceRetrieval.Providers;
using PriceRetrieval.Providers.Bitfinex;
using PriceRetrieval.Providers.Bitstamp;
using PriceRetrieval.Services;
using PriceRetrieval.Services.Decorators;

namespace PriceRetrieval
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IAggregator, Aggregator>();
            services.AddSingleton<ITimeSpanParser, TimeSpanParser>();
            services.AddSingleton<IPairPriceDataStorage, PairPriceDataStorage>();

            services.AddTransient<BitfinexApiClient>();
            services.AddTransient<BitstampApiClient>();
            services.AddSingleton<IProviderApiClientFactory, ProviderApiClientFactory>();

            services.AddTransient<IPriceRetrievalService, PriceRetrievalService>();
            services.Decorate<IPriceRetrievalService, PriceRetrievalServiceErrorHandlingDecorator>();

            // get pair commands
            services.AddTransient<IGetPairPriceCommand, GetPairPriceCommand>();

            // get pair request validators
            services.AddSingleton<PriceRetrievalValidator>();

            //memorized pairs database
            services.AddSingleton<IPairPriceDataStorage, PairPriceDataStorage>();

            return services;
        }
    }
}