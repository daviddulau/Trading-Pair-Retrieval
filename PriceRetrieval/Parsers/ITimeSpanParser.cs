namespace PriceRetrieval.Parsers
{
    public interface ITimeSpanParser
    {
        TimeSpan? Parse(string? value);
    }
}
