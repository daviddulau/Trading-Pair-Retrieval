namespace PriceRetrieval.Commands.Implementations.GetPairPriceCommand
{
    public interface IAggregator
    {
        double ProcessAggregation(List<double> pairs);
    }
}
