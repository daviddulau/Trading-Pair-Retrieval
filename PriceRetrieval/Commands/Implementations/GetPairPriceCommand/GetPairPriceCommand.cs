using System.Net;
using PriceRetrieval.DataStorage;
using PriceRetrieval.Models;
using PriceRetrieval.Models.Constants;
using PriceRetrieval.Providers;
using PriceRetrieval.Providers.Bitfinex;
using PriceRetrieval.Providers.Bitstamp;

namespace PriceRetrieval.Commands.Implementations.GetPairPriceCommand
{
    public class GetPairPriceCommand : IGetPairPriceCommand
    {
        private readonly IProviderApiClientFactory _providerApiClientFactory;
        private readonly IAggregator _aggregator;
        private readonly IPairPriceDataStorage _pairPriceDataStorage;

        public GetPairPriceCommand(
            IProviderApiClientFactory providerApiClientFactory,
            IAggregator aggregator, 
            IPairPriceDataStorage pairPriceDataStorage)
        {
            _providerApiClientFactory = providerApiClientFactory;
            _aggregator = aggregator;
            _pairPriceDataStorage = pairPriceDataStorage;
        }

        public async Task<PriceRetrievalResponse> Execute(PriceRetrievalRequest request)
        {
            ApproximateTimeToHourly(request);

            //check in-house
            var storedValue = _pairPriceDataStorage.GetPair(request);

            if (!string.IsNullOrWhiteSpace(storedValue))
            {
                // return success
                return new PriceRetrievalResponse
                {
                    ClosePrice = Convert.ToDouble(storedValue),
                    request = request,
                    Result = new PriceRetrievalResult
                    {
                        Code = HttpStatusCode.OK,
                        Description = ResultDescriptions.SucceededWithInHouseData
                    }
                };
            }

            //get from externals
            var pairs = await GetPairPricesFromProviders(request);

            if (pairs.Count == 0)
            {
                // return no providers responded
                return new PriceRetrievalResponse
                {
                    ClosePrice = 0,
                    request = request,
                    Result = new PriceRetrievalResult
                    {
                        Code = HttpStatusCode.NoContent,
                        Description = ResultDescriptions.NoProvidersResponded
                    }
                };
            }

            var closePrice = _aggregator.ProcessAggregation(pairs);

            //save pair data
            _pairPriceDataStorage.InsertPairCommand(request, closePrice);

            // return success
            return new PriceRetrievalResponse
            {
                ClosePrice = closePrice,
                request = request,
                Result = new PriceRetrievalResult
                {
                    Code = HttpStatusCode.OK,
                    Description = ResultDescriptions.Succeeded
                }
            };
        }

        private async Task<List<double>> GetPairPricesFromProviders(PriceRetrievalRequest request)
        {
            var pairs = new List<double>();

            var bitfinexApiClient = _providerApiClientFactory.CreateProvider(BitfinexApiClient.ProviderName);
            var bitfinexResponse = await bitfinexApiClient.GetPairPrice(request);

            if (bitfinexResponse.IsSuccessStatusCode)
            {
                pairs.Add(bitfinexResponse.Data);
            }

            var bitstampApiClient = _providerApiClientFactory.CreateProvider(BitstampApiClient.ProviderName);
            var bitstampResponse = await bitstampApiClient.GetPairPrice(request);

            if (bitstampResponse.IsSuccessStatusCode)
            {
                pairs.Add(bitstampResponse.Data);
            }

            return pairs;
        }

        private void ApproximateTimeToHourly(PriceRetrievalRequest request)
        {
            const string zeroMinutesAndSeconds = ":00:00";
            if (!request.DateTime.Contains(zeroMinutesAndSeconds))
            {
                //remove minutes and seconds, and let only date and hour precision
                request.DateTime = request.DateTime.Substring(0, request.DateTime.IndexOf(":")) + zeroMinutesAndSeconds;
            }
        }
    }
}
