using Microsoft.Extensions.DependencyInjection;
using PriceRetrieval.DataStorage;

namespace PriceRetrieval
{
    public static class InitializeDatabase
    {
        public static IServiceCollection CreateDatabase(this IServiceCollection services)
        {
            try
            {
                var pairPriceDataStorage = new PairPriceDataStorage();

                pairPriceDataStorage.CreateTableCommand();
            }
            catch
            {
            }

            return services;
        }
    }
}
