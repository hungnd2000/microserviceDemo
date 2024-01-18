using Product.API.Application.Repositories;
using Product.API.Application.Services;
using Product.API.Entities;

namespace Product.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductItem> AddProductAsync(ProductItem product)
        {
            return await _repository.AddProductAsync(product);
        }

        public async Task<ProductItem> GetProductByIdAsync(int id)
        {
            return await _repository.GetProductByIdAsync(id);
        }

        public async Task<List<ProductItem>> GetProductsAsync()
        {
            return await _repository.GetProductsAsync();
        }

        public async Task<ProductItem> UpdateAvailableQuantityAsync(int id, int availableQuantity)
        {
            return await _repository.UpdateAvailableQuantityAsync(id, availableQuantity);
        }

        public async Task<ProductItem> UpdateNameAsync(int id, string name)
        {
            return await _repository.UpdateNameAsync(id, name);
        }

        public async Task<ProductItem> UpdatePriceAsync(int id, decimal price)
        {
            return await _repository.UpdatePriceAsync(id, price);
        }

        public async Task<ProductItem> UpdateProductAsync(int id, ProductItem product)
        {
            return await _repository.UpdateProductAsync(id, product);
        }
    }
}
