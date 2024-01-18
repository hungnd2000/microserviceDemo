using Microsoft.EntityFrameworkCore;
using Product.API.Application.Repositories;
using Product.API.Data;
using Product.API.Entities;

namespace Product.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(ProductDbContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ProductItem> AddProductAsync(ProductItem product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return _context.Products.Find(product);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return null;
            }
        }

        public async Task<ProductItem> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<List<ProductItem>> GetProductsAsync()
        {
            try
            {
                return await _context.Products.ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return null;
            }
        }

        public async Task<ProductItem> UpdateAvailableQuantityAsync(int id, int availableQuantity)
        {

            try
            {
                var productUpdate = await GetProductByIdAsync(id);
                if (productUpdate != null)
                {
                    productUpdate.AvailableQuantity = availableQuantity;
                    _context.Products.Update(productUpdate);
                    await _context.SaveChangesAsync();
                    return productUpdate;
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return null;
            }
        }

        public async Task<ProductItem> UpdateNameAsync(int id, string name)
        {
            try
            {
                var productUpdate = await GetProductByIdAsync(id);
                if (productUpdate != null)
                {
                    productUpdate.Name = name;
                    _context.Products.Update(productUpdate);
                    await _context.SaveChangesAsync();
                    return productUpdate;
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return null;
            }

        }

        public async Task<ProductItem> UpdatePriceAsync(int id, decimal price)
        {
            try
            {
                var productUpdate = await GetProductByIdAsync(id);
                if (productUpdate != null)
                {
                    productUpdate.Price = price;
                    _context.Products.Update(productUpdate);
                    await _context.SaveChangesAsync();
                    return productUpdate;
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return null;
            }
        }

        public async Task<ProductItem> UpdateProductAsync(int id, ProductItem product)
        {
            try
            {
                var productEdit = await _context.Products.FindAsync(id);
                if (productEdit != null)
                {
                    productEdit.Id = id;
                    productEdit.Name = product.Name;
                    productEdit.Price = product.Price;
                    productEdit.AvailableQuantity = product.AvailableQuantity;

                    _context.Products.Update(productEdit);
                    await _context.SaveChangesAsync();
                    return productEdit;
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return null;
            }
        }

    }
}
