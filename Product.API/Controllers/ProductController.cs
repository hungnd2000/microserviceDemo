using Microsoft.AspNetCore.Mvc;
using Product.API.Application.Services;
using Product.API.DTOs;
using Product.API.Entities;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<ProductDTO>> GetProducts() {
            var listProductDTO = new List<ProductDTO>();
            var products = await _service.GetProductsAsync();

            foreach (var product in products)
            {
                var productDTO = new ProductDTO()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    AvailableQuantity = product.AvailableQuantity
                };

                listProductDTO.Add(productDTO);
            }
            return listProductDTO;
        }

        [HttpPost]
        public async Task<ProductItem> AddProductAsync(ProductDTO productDTO)
        {
            var productAdd = new ProductItem()
            {
                Id = productDTO.Id,
                Name = productDTO.Name,
                Price = productDTO.Price,   
                AvailableQuantity = productDTO.AvailableQuantity
            };

            return await _service.AddProductAsync(productAdd);
        }

        [HttpPut]
        public async Task<ProductItem> UpdateProductAsync(int Id, ProductDTO productDTO)
        {
            var productUpdate = new ProductItem()
            {
                Id = productDTO.Id,
                Name = productDTO.Name,
                Price = productDTO.Price,
                AvailableQuantity = productDTO.AvailableQuantity
            };

            return await _service.UpdateProductAsync(Id, productUpdate);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ProductItem> GetProductByIdAsync(int id)
        {
            return await _service.GetProductByIdAsync(id);
        }

        [HttpPatch]
        [Route("availableQuantity/{id}")]
        public async Task<IActionResult> UpdateProductAvailableQuantity(int id, ProductAvailableQuantityDTO productAvailableQuantityDTO)
        {
            var data = await _service.UpdateAvailableQuantityAsync(id, productAvailableQuantityDTO.AvailableQuantity);
            return Ok(data);
        }

        [HttpPatch]
        [Route("name/{id}")]
        public async Task<IActionResult> UpdateProductName(int id, ProductNameDTO productNameDTO)
        {
            var data = await _service.UpdateNameAsync(id, productNameDTO.Name);
            return Ok(data);
        }

        [HttpPatch]
        [Route("price/{id}")]
        public async Task<IActionResult> UpdateProductPrice(int id, ProductPriceDTO productPriceDTO)
        {
            var data = await _service.UpdatePriceAsync(id, productPriceDTO.Price);
            return Ok(data);
        }

    }
}
