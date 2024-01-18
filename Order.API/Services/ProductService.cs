using Order.API.Application.Services;
using Order.API.DTOs;
using System.Text.Json;
using System.Text;
using System;
using Order.API.Entities;

namespace Order.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<ProductService> _logger;
        private readonly HttpClient _client;
        public ProductService(IConfiguration config, ILogger<ProductService> logger, HttpClient client)
        {
            _config = config;
            _logger = logger;
            _client = client;

        }

        public void UpdateProductQuantity(List<ProductUpdateQuantity> listProductUpdateQuantity)
        {
            try
            {
                for (int i = 0; i < listProductUpdateQuantity.Count; i++)
                {
                    // Gọi hàm PATCH
                    PatchData(listProductUpdateQuantity[i].ProductId, listProductUpdateQuantity[i]);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }

        public async void PatchData(int resourceId, ProductUpdateQuantity updateData)
        {

            // Tạo URL PATCH với ID của đối tượng cần cập nhật
            string patchUrl = _config["HttpGetProduct"] + "/availableQuantity/" + resourceId;
            ProductItem product = GetQuantityByProductId(resourceId).Result;

            if (product != null)
            {
                var updateQuantityData = new ProductUpdateQuantity()
                {
                    ProductId = updateData.ProductId,
                    AvailableQuantity = product.AvailableQuantity - updateData.AvailableQuantity,
                };

                // Chuyển đối tượng UpdateData thành chuỗi JSON
                string jsonData = JsonSerializer.Serialize(updateQuantityData);

                // Tạo nội dung PATCH request
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Thực hiện PATCH request
                _client.PatchAsync(patchUrl, content);
            }

        }

        public async Task<ProductItem> GetQuantityByProductId(int id)
        {

            string ApiGetProductById = _config["HttpGetProduct"] + "/" + id;
            HttpResponseMessage response = new HttpResponseMessage();

            response = await _client.GetAsync(ApiGetProductById);
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    if (response.Content.Headers.ContentLength != 0)
                    {
                        var product = await response.Content.ReadFromJsonAsync<ProductItem>();
                        return product;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}
