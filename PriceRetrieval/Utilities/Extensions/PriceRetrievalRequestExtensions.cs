using System.Net;
using FluentValidation.Results;
using PriceRetrieval.Commands.Validators.Extensions;
using PriceRetrieval.Models;
using PriceRetrieval.Models.Constants;

namespace PriceRetrieval.Utilities.Extensions
{
    public static class PriceRetrievalRequestExtensions
    {
        public static PriceRetrievalResponse ToErrorResponse(this PriceRetrievalRequest request)
        {
            return new PriceRetrievalResponse
            {
                request = request,
                Result = new ()
                {
                    Code = HttpStatusCode.InternalServerError,
                    Description = ResultDescriptions.UnexpectedError
                }
            };
        }

        public static PriceRetrievalResponse ToInvalidResponse(
            this PriceRetrievalRequest request, 
            ValidationResult validationResult)
        {
            return new PriceRetrievalResponse
            {
                request = request,
                Result = new ()
                {
                    Code = HttpStatusCode.BadRequest,
                    Description = ResultDescriptions.DeclinedForUnknownReason,
                    ParameterErrors = validationResult.ToParameterErrors()
                }
            };
        }
    }
}
