using PriceRetrieval.Models;
using PriceRetrieval.Parsers;

namespace PriceRetrieval.Providers.Bitstamp
{
    public class BitstampApiClient : IProviderApiClient
    {
        private const string baseURI = "https://www.bitstamp.net/";
        private readonly ITimeSpanParser _timeSpanParser;

        public static string ProviderName => "Bitstamp";

        public BitstampApiClient(
            ITimeSpanParser timeSpanParser)
        {
            _timeSpanParser = timeSpanParser;
        }

        public async Task<Result<double>> GetPairPrice(PriceRetrievalRequest request)
        {
            HttpGetRequestor requestor = new();

            var startTime = _timeSpanParser.Parse(request.DateTime);
            var pair = request.Pair.ToLower();

            var requestUri =
                $"{baseURI}api/v2/ohlc/{pair}" +
                $"/?step=3600&limit=1&start={startTime.Value.TotalSeconds}";

            static string ResponseProcessor(string responseBody)
            {
                var startIndex = responseBody
                    .IndexOf(":", 
                        responseBody.LastIndexOf("{"));

                var value = responseBody
                    .Substring(startIndex + 2,
                        responseBody.IndexOf(",", startIndex) - startIndex - 2);

                return value
                    .Replace("\"", "");
            };

            var getPairPriceResult = await requestor.GetPairPrice(requestUri, ResponseProcessor);

            if (!getPairPriceResult.IsSuccessStatusCode)
            {
                return Result.Error<double>(getPairPriceResult.Message);
            }

            return Result.Success(getPairPriceResult.Data);
        }
    }
}
