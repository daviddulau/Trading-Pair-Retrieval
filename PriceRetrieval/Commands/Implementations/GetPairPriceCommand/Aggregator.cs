namespace PriceRetrieval.Commands.Implementations.GetPairPriceCommand
{
    public class Aggregator : IAggregator
    {
        public double ProcessAggregation(List<double> pairs)
        {
            return pairs.Aggregate((acc, num) => acc + num) / pairs.Count;
        }
    }
}
