namespace Order.API.Entities
{
    public class OrderModel
    {
        public string Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string AdditionalAddress { get; set; }
        public List<OrderItem> Items { get; set; }
        public OrderModel()
        {
            Items = new List<OrderItem>();
        }
    }
}
