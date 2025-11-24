using AggregatorService.DTO;

namespace AggregatorService.Services
{
    public class AggregationService : IAggregationService
    {
        private readonly HttpClient _ordersClient;
        private readonly HttpClient _reviewsClient;

        public AggregationService(IHttpClientFactory factory)
        {
            _ordersClient = factory.CreateClient("orders");
            _reviewsClient = factory.CreateClient("reviews");
        }

        public async Task<OrderWithReviewDto> GetOrderWithReviewAsync(int orderId)
        {
            var order = await _ordersClient.GetFromJsonAsync<OrderWithReviewDto>($"api/Orders/Order/{orderId}");
            if (order == null)
                throw new Exception("Order not found");

            var reviews = await _reviewsClient.GetFromJsonAsync<List<ReviewDto>>($"api/Reviews/order/{orderId}");
            var review = reviews?.FirstOrDefault();

            order.Review = review;

            return order;
        }
    }
}
