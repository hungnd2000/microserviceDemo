namespace Order.API.DTOs
{
    public class CustomerBasket
    {
        public int CusTomerId { get; set; }
        public List<BasketItem> Items { get; set; }

        public CustomerBasket()
        {
            Items = new List<BasketItem>();
        }
    }
}
