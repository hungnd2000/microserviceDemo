using Product.API.Entities;

namespace Product.API.Data
{
    public class ProductDbContextSeed
    {
        public async Task SeedAsync(ProductDbContext context, ILogger<ProductDbContextSeed> logger)
        {
            if (!context.Products.Any())
            {
                logger.LogInformation("Generate default data for Product table");
                context.Products.AddRange(GetPredefinedProduct());
                await context.SaveChangesAsync();
            }
        }

        private List<ProductItem> GetPredefinedProduct()
        {
            return new List<ProductItem>()
            {
            new ProductItem {Name = "ProductItem 1",Price=1000000,AvailableQuantity=20},
            new ProductItem {Name = "ProductItem 2",Price=2999999,AvailableQuantity=15},
            new ProductItem {Name = "ProductItem 3",Price=1999999,AvailableQuantity=5},
            new ProductItem {Name = "ProductItem 4",Price=2000000,AvailableQuantity=10},
            new ProductItem {Name = "Product 5",Price=3999999,AvailableQuantity=0}
            };
        }
    }
}
