using PriceRetrieval.Models.Errors;
using System.Net;

namespace PriceRetrieval.Models
{
    public class PriceRetrievalResult
    {
        public HttpStatusCode Code { get; set; }
        public string? Description { get; set; }
        public IEnumerable<ParameterError>? ParameterErrors { get; set; }
    }
}
