using Product.API.DTOs;

namespace Basket.API.DTOs
{
    public class ProductUpdateQuantity
    {
        public int ProductId { get; set; }
        public int AvailableQuantity { get; set; }
    }
}
