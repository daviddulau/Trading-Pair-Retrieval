using Microsoft.AspNetCore.Mvc;
using PriceRetrieval.Models;
using PriceRetrieval.Services;

namespace PriceRetrieval.API.Controllers.v1
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class PriceRetrievalController : ControllerBase
    {
        private readonly IPriceRetrievalService _pairRetrievalService;

        public PriceRetrievalController(
            IPriceRetrievalService pairRetrievalService)
        {
            _pairRetrievalService = pairRetrievalService;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<PriceRetrievalResponse>> Get([FromQuery]string pair, [FromQuery] string dateTime)
        {
            var request = new PriceRetrievalRequest
            {
                Pair = pair,
                DateTime = dateTime
            };

            var result = await _pairRetrievalService.GetPairPrice(request);
            return Ok(result);
        }
    }
}
