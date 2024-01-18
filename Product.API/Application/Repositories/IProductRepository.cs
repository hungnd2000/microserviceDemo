using Product.API.Entities;

namespace Product.API.Application.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductItem>> GetProductsAsync();
        Task<ProductItem> AddProductAsync(ProductItem product);
        Task<ProductItem> UpdateProductAsync(int id, ProductItem product);
        Task<ProductItem> GetProductByIdAsync(int id);
        Task<ProductItem> UpdateAvailableQuantityAsync(int id, int availableQuantity);
        Task<ProductItem> UpdatePriceAsync(int id, decimal price);
        Task<ProductItem> UpdateNameAsync(int id, string name);

    }
}
