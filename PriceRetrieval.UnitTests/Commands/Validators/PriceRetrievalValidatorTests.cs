using FluentValidation.TestHelper;
using PriceRetrieval.Commands.Validators;
using PriceRetrieval.Models;

namespace PriceRetrieval.UnitTests.Commands.Validators
{
    
    public class PriceRetrievalValidatorTests
    {
        [Fact]
        public void Validate_ShouldNotHaveAnyErrors_WhenRequestIsValid()
        {
            var validator = CreateValidator();
            var request = CreateValidRequest();

            var validationResult = validator.TestValidate(request);

            validationResult.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_ShouldHaveErrorForPair_WhenNull()
        {
            var validator = CreateValidator();
            var request = CreateValidRequest();
            request.Pair = string.Empty;

            var validationResult = validator.TestValidate(request);

            validationResult.ShouldHaveValidationErrorFor(r => r.Pair);
        }

        [InlineData("btcus")]
        [InlineData("btc-usd")]
        [InlineData("BTC-usd")]
        [InlineData("btc-usd")]
        [InlineData("btc/usd")]
        public void Validate_ShouldHaveErrorForPair_WhenPairIsWrong(string pair)
        {
            var validator = CreateValidator();
            var request = CreateValidRequest();
            request.Pair = pair;

            var validationResult = validator.TestValidate(request);

            validationResult.ShouldHaveValidationErrorFor(r => r.Pair);
        }

        [Fact]
        public void Validate_ShouldNotHaveAnyErrors_WhenPairIsUpperCase()
        {
            var validator = CreateValidator();
            var request = CreateValidRequest();
            request.Pair = request.Pair.ToUpper();

            var validationResult = validator.TestValidate(request);

            validationResult.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_ShouldHaveErrorForDateTime_WhenNull()
        {
            var validator = CreateValidator();
            var request = CreateValidRequest();
            request.DateTime = string.Empty;

            var validationResult = validator.TestValidate(request);

            validationResult.ShouldHaveValidationErrorFor(r => r.DateTime);
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenDateTimeIsLowerCase()
        {
            var validator = CreateValidator();
            var request = CreateValidRequest();
            request.DateTime = request.DateTime.ToLower();

            var validationResult = validator.TestValidate(request);

            validationResult.ShouldHaveValidationErrorFor(r => r.DateTime);
        }

        [Theory]
        [InlineData("203-01-01T12:00:00")]
        [InlineData("202301-01T12:00:00")]
        [InlineData("2023-01-01T12:00")]
        [InlineData("2023-01-01 12:00:00")]
        [InlineData("2023-01-01T00")]
        public void Validate_ShouldHaveAnyErrors_WhenDateTimeIsWrong(string datetime)
        {
            var validator = CreateValidator();
            var request = CreateValidRequest();
            request.DateTime = datetime;

            var validationResult = validator.TestValidate(request);

            validationResult.ShouldHaveValidationErrorFor(r => r.DateTime);
        }

        private static PriceRetrievalValidator CreateValidator()
        {
            return new PriceRetrievalValidator();
        }

        private static PriceRetrievalRequest CreateValidRequest()
        {
            return new PriceRetrievalRequest
            {
                Pair = "btcusd",
                DateTime = "2023-01-01T12:00:00"
            };
        }
    }
}