using PriceRetrieval.Models.Constants;
using System.Globalization;

namespace PriceRetrieval.Providers
{
    public class HttpGetRequestor : IHttpGetRequestor
    {
        private readonly CultureInfo _usCultureInfo = CultureInfo.CreateSpecificCulture("en-US");

        public async Task<Result<double>> GetPairPrice(string requestUri, Func<string, string> responseProcessor)
        {
            var httpClient = new HttpClient();
            HttpResponseMessage httpResponse;

            try
            {
                httpResponse = await httpClient.GetAsync(requestUri);
            }
            catch
            {
                return Result.Error<double>($"Failed to connect to api via endpoint {requestUri}.");
            }

            if (!httpResponse.IsSuccessStatusCode)
            {
                return Result.Error<double>(ResultDescriptions.DeclinedForUnknownReason);
            }
            var responseBody = await httpResponse.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(responseBody))
            {
                return Result.Error<double>("No content");
            }
            
            responseBody = responseProcessor(responseBody);
            return Result.Success(Convert.ToDouble(responseBody, _usCultureInfo));
        }
    }
}
