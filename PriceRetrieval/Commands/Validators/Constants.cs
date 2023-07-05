namespace PriceRetrieval.Commands.Validators
{
    public static class Constants
    {
        public const string DateTimeRegex = "20([0-9]{2})-(0[1-9]|1[0-2])-(0[1-9]|1[0-9]|2[0-9]|3[0-1])T([0-2][0-9]):([0-5][0-9]):([0-5][0-9])";
        public const string PairRegex = "([a-zA-Z]{3}[a-zA-Z]{3})";
    }
}
