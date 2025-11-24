namespace AggregatorService.DTO
{
    public class OrderWithReviewDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }

        public IEnumerable<OrderItemDto> Items { get; set; }

        public ReviewDto? Review { get; set; }
    }
}
