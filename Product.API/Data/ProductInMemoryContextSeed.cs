using Microsoft.EntityFrameworkCore;

namespace Product.API.Data
{
    public class ProductInMemoryContextSeed
    {
        public async Task SeedAsync(ProductInMemoryContext inMemoryContext, ProductDbContext dbContext)
        {
            var products = await dbContext.Products.ToListAsync();
            foreach (var prod in products)
            {
                inMemoryContext.Products.Add(prod.Id, prod);
            }
        }
    }
}
