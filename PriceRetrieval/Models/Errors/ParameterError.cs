using System.Text.Json.Serialization;

namespace PriceRetrieval.Models.Errors
{
    public class ParameterError
    {
        public required string Name { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public object? Value { get; set; }

        public required string Message { get; set; }
    }
}
