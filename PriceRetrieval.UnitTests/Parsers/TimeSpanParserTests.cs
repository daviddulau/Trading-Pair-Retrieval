using PriceRetrieval.Parsers;

namespace PriceRetrieval.UnitTests.Parsers
{
    public class TimeSpanParserTests
    {
        private readonly DateTime _defaultDateTime = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        [Theory]
        [InlineData("2000-01-01T00:00:00", 2000, 1, 1)]
        [InlineData("2000-12-12T00:00:00", 2000, 12, 12)]
        public void TimeSpanParser_ShouldReturnExpectedTimeSpan_WhenCorrectFormat(string? value, int expectedYear, int expectedMonth, int expectedDay)
        {
            //Arrange
            var parser = new TimeSpanParser();
            var expectedDate = new TimeSpan(new DateTime(expectedYear, expectedMonth, expectedDay, 0, 0, 0, DateTimeKind.Utc).Ticks - _defaultDateTime.Ticks);

            //Act
            var actualResult = parser.Parse(value);

            //Assert
            actualResult.Should().Be(expectedDate);
        }
    }
}
