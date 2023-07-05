namespace PriceRetrieval.Models.Constants
{
    public static class ResultDescriptions
    {
        public const string SucceededWithInHouseData = "request succeeded with cached data";
        public const string Succeeded = "request succeeded";
        public const string DeclinedForUnknownReason = "request declined as invalid or missing parameters";
        public const string UnexpectedError = "unexpected error (request could not be processed)";
        public const string NoProvidersResponded = "request could not get any pairs";
    }
}
