using AggregatorService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AggregatorService.Controllers
{
    [ApiController]
    [Route("api/aggregation")]
    public class AggregationController : ControllerBase
    {
        private readonly IAggregationService _service;

        public AggregationController(IAggregationService service)
        {
            _service = service;
        }

        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetFullOrder(int orderId)
        {
            var result = await _service.GetOrderWithReviewAsync(orderId);
            return Ok(result);
        }
    }
}
