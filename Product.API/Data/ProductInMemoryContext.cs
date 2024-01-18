using Product.API.Entities;

namespace Product.API.Data
{
    public class ProductInMemoryContext
    {
        public ProductInMemoryContext()
        {
            Products = new Dictionary<int, ProductItem>();
        }
        public Dictionary<int, ProductItem> Products { get; set; } = new Dictionary<int, ProductItem>();

    }
}
