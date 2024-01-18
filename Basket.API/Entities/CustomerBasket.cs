namespace Basket.API.Entities
{
    public class CustomerBasket
    {
        public int CustomerId { get; set; }
        public List<BasketItem> Items { get; set; }
        public CustomerBasket()
        {
            Items = new List<BasketItem>();
        }
    }
}
