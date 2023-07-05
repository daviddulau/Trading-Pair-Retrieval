namespace PriceRetrieval.Parsers
{
    /// <summary>
    /// Parses datetimes from the exact format yyyy-MM-dd HH:mm:ss to Unix epoch at seconds
    /// </summary>
    public class TimeSpanParser : ITimeSpanParser
    {
        private readonly DateTime _defaultDateTime = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public TimeSpan? Parse(string? value)
        {
            if (value == null) return null;

            var dateTime = Convert.ToDateTime(value);

            var epoch = dateTime - _defaultDateTime;

            return epoch;
        }
    }
}
