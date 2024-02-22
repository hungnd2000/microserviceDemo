namespace Basket.API.DTOs
{
    public class OrderItemDTO
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
