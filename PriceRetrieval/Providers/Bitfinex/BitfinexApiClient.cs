using PriceRetrieval.Models;
using PriceRetrieval.Parsers;

namespace PriceRetrieval.Providers.Bitfinex
{
    public class BitfinexApiClient : IProviderApiClient
    {
        private const string baseURI = "https://api-pub.bitfinex.com/";
        private readonly ITimeSpanParser _timeSpanParser;

        public static string ProviderName => "Bitfinex";

        public BitfinexApiClient(
            ITimeSpanParser timeSpanParser)
        {
            _timeSpanParser = timeSpanParser;
        }

        public async Task<Result<double>> GetPairPrice(PriceRetrievalRequest request)
        {
            HttpGetRequestor requestor = new();

            var startTime = _timeSpanParser.Parse(request.DateTime);
            var endTime = startTime.Value + TimeSpan.FromHours(1);
            var pair = request.Pair.ToUpper();

            var requestUri =
                $"{baseURI}v2/candles/trade:1h:t{pair}" +
                $"/hist?start={startTime.Value.TotalMilliseconds}&end={endTime.TotalMilliseconds}&limit=1";

            static string ResponseProcessor(string responseBody) => responseBody.Split(",")[2];

            var getPairPriceResult = await requestor.GetPairPrice(requestUri, ResponseProcessor);

            if (!getPairPriceResult.IsSuccessStatusCode)
            {
                return Result.Error<double>(getPairPriceResult.Message);
            }

            return Result.Success(getPairPriceResult.Data);
        }
    }
}
