using Basket.API.Application.Repositories;
using Basket.API.Application.Services;
using Basket.API.DTOs;
using Basket.API.Entities;

namespace Basket.API.Services
{
    public class CustomerBasketService : ICustomerBasketService
    {
        private readonly ICustomerBasketRepository _repository;
        private readonly ILogger<CustomerBasketService> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public CustomerBasketService
            (
            ICustomerBasketRepository      repository, 
            ILogger<CustomerBasketService> logger, 
            IConfiguration                 configuration,
            HttpClient httpClient
            )
        {
            _repository = repository;
            _logger        = logger;
            _configuration = configuration;
            _httpClient    = httpClient;   
        }

        public async Task<UpsetCustomerBasketResponseDTO> AddAsync(UpsetCustomerBasketDTO upsetCustomerBasketDTO)
        {
            //var client = _httpClient.CreateClient();
            string apiGetProductId = _configuration["HttpGetProduct"] + "/" + upsetCustomerBasketDTO.ProductId;
            CustomerBasket customerBasket = new CustomerBasket();
            UpsetCustomerBasketResponseDTO upsetCustomerBasketResponseDTO = new UpsetCustomerBasketResponseDTO("", customerBasket);

            HttpResponseMessage responseMessage = new HttpResponseMessage();
            responseMessage = await _httpClient.GetAsync(apiGetProductId);
            if (responseMessage.IsSuccessStatusCode)
            {
                if (responseMessage.Content.Headers.ContentLength != 0)
                {
                    var product = await responseMessage.Content.ReadFromJsonAsync<ProductDTO>();
                    if (product.AvailableQuantity < upsetCustomerBasketDTO.Quantity)
                    {
                        upsetCustomerBasketResponseDTO.Data = null;
                        upsetCustomerBasketResponseDTO.Message = "Số lượng sản phẩm không đủ để thêm vào giỏ hàng.";
                        return upsetCustomerBasketResponseDTO;
                    }
                    else if (upsetCustomerBasketDTO.Quantity == 0)
                    {
                        await _repository.RemoveCustomerBasketByCustomerIdAsync(upsetCustomerBasketDTO.CustomerId);
                        upsetCustomerBasketResponseDTO.Data = null;
                        upsetCustomerBasketResponseDTO.Message = "Đã xóa giỏ hàng";
                        return upsetCustomerBasketResponseDTO;
                    } 
                    else
                    {
                        var customerBasket1 = await _repository.GetCustomerBasketByIdAsync(upsetCustomerBasketDTO.CustomerId);
                        int remainQuantity = product.AvailableQuantity - upsetCustomerBasketDTO.Quantity;
                        customerBasket.CustomerId = upsetCustomerBasketDTO.CustomerId;
                        BasketItem basketItem = new BasketItem()
                        {
                            ProductId = upsetCustomerBasketDTO.ProductId,
                            ProductName = product.Name,
                            Quantity = upsetCustomerBasketDTO.Quantity,
                            Status = 1
                        };
                        customerBasket.Items.Add(basketItem);

                        if (customerBasket1 != null)
                        {
                            if (customerBasket1.Items.Any(e => e.ProductId == upsetCustomerBasketDTO.ProductId))
                            {
                                for (int i = 0; i < customerBasket1.Items.Count(); i++)
                                {
                                    if (customerBasket1.Items[i].ProductId == upsetCustomerBasketDTO.ProductId)
                                    {
                                        upsetCustomerBasketResponseDTO.Data = await _repository.UpdateBasketItemAsync(upsetCustomerBasketDTO.CustomerId, upsetCustomerBasketDTO.ProductId, upsetCustomerBasketDTO.Quantity);
                                        upsetCustomerBasketResponseDTO.Message = "Cap Nhat BasketItem";
                                    }
                                }
                                return upsetCustomerBasketResponseDTO;
                            }
                            else
                            {
                                upsetCustomerBasketResponseDTO.Data = await _repository.AddBasketItemAsync(customerBasket);
                                upsetCustomerBasketResponseDTO.Message = "Them moi BasketItem";
                                return upsetCustomerBasketResponseDTO;
                            }
                        }
                        else
                        {
                            upsetCustomerBasketResponseDTO.Data = await _repository.AddCustomerBasketAsync(customerBasket);
                            upsetCustomerBasketResponseDTO.Message = "Them moi BasketCustomer";
                            return upsetCustomerBasketResponseDTO;
                        }
                    }
                }
                return null;
            }
            return null;
        }

        public async Task<List<CustomerBasket>> GetAllCustomerBasketAsync()
        {
            return await _repository.GetAllCustomerBasketAsync();
        }

        public async Task<CustomerBasket> GetBasketByIdAsync(int id)
        {
            return await _repository.GetCustomerBasketByIdAsync(id);
        }

        public async Task<UpsetCustomerBasketResponseDTO> IncreaseItemQuantityAsync(UpsetCustomerBasketDTO upsetCustomerBasketDTO)
        {
            //var client = _httpClient.CreateClient();
            string apiGetProductId = _configuration["HttpGetProduct"] + "/" + upsetCustomerBasketDTO.ProductId;
            CustomerBasket customerBasket = new CustomerBasket();
            UpsetCustomerBasketResponseDTO upsetCustomerBasketResponseDTO = new UpsetCustomerBasketResponseDTO("", customerBasket);

            HttpResponseMessage responseMessage = new HttpResponseMessage();
            responseMessage = await _httpClient.GetAsync(apiGetProductId);
            if (responseMessage.IsSuccessStatusCode)
            {
                if (responseMessage.Content.Headers.ContentLength != 0)
                {
                    var product = await responseMessage.Content.ReadFromJsonAsync<ProductDTO>();
                    customerBasket = await _repository.GetCustomerBasketByIdAsync(upsetCustomerBasketDTO.CustomerId);
                    var basketItem = customerBasket.Items.Find(i => i.ProductId == upsetCustomerBasketDTO.ProductId);

                    if(basketItem == null)
                    {
                        upsetCustomerBasketResponseDTO.Data = null;
                        upsetCustomerBasketResponseDTO.Message = "Không có Item này trong giỏ hảng.";
                        return upsetCustomerBasketResponseDTO;
                    }
                    else
                    {
                        var quantityUpdate = upsetCustomerBasketDTO.Quantity + basketItem.Quantity;
                        if (product.AvailableQuantity < quantityUpdate)
                        {
                            upsetCustomerBasketResponseDTO.Data = null;
                            upsetCustomerBasketResponseDTO.Message = "Số lượng sản phẩm không đủ để thêm vào giỏ hàng.";
                            return upsetCustomerBasketResponseDTO;
                        }
                        else
                        {
                            upsetCustomerBasketResponseDTO.Data = await _repository.UpdateBasketItemAsync(upsetCustomerBasketDTO.CustomerId, upsetCustomerBasketDTO.ProductId, quantityUpdate);
                            upsetCustomerBasketResponseDTO.Message = "Đã tăng số lượng Item.";
                            return upsetCustomerBasketResponseDTO;
                        }
                    }
                }
                return null;
            }
            return null;
        }

        public async Task<UpsetCustomerBasketResponseDTO> ReduceItemQuantityAsync(UpsetCustomerBasketDTO upsetCustomerBasketDTO)
        {
            //var client = _httpClient.CreateClient();
            string apiGetProductId = _configuration["HttpGetProduct"] + "/" + upsetCustomerBasketDTO.ProductId;
            CustomerBasket customerBasket = new CustomerBasket();
            UpsetCustomerBasketResponseDTO upsetCustomerBasketResponseDTO = new UpsetCustomerBasketResponseDTO("", customerBasket);

            HttpResponseMessage responseMessage = new HttpResponseMessage();
            responseMessage = await _httpClient.GetAsync(apiGetProductId);
            if (responseMessage.IsSuccessStatusCode)
            {
                if (responseMessage.Content.Headers.ContentLength != 0)
                {
                    var product = await responseMessage.Content.ReadFromJsonAsync<ProductDTO>();
                    customerBasket = await _repository.GetCustomerBasketByIdAsync(upsetCustomerBasketDTO.CustomerId);
                    var basketItem = customerBasket.Items.Find(i => i.ProductId == upsetCustomerBasketDTO.ProductId);

                    if(basketItem == null)
                    {
                        upsetCustomerBasketResponseDTO.Data = null;
                        upsetCustomerBasketResponseDTO.Message = "Không có Item này trong giỏ hảng.";
                        return upsetCustomerBasketResponseDTO;
                    }
                    else
                    {
                        var quantityUpdate = basketItem.Quantity - upsetCustomerBasketDTO.Quantity;
                        if (quantityUpdate < 0)
                        {
                            CustomerBasket result = await _repository.UpdateBasketItemAsync(upsetCustomerBasketDTO.CustomerId, upsetCustomerBasketDTO.ProductId, 0);
                            var itemCount = result.Items.Count;
                            if (itemCount > 0)
                            {
                                upsetCustomerBasketResponseDTO.Data = result;
                                upsetCustomerBasketResponseDTO.Message = "Đã xóa Item khỏi giỏ hàng";
                                return upsetCustomerBasketResponseDTO;
                            }

                            await _repository.RemoveCustomerBasketByCustomerIdAsync(upsetCustomerBasketDTO.CustomerId);
                            upsetCustomerBasketResponseDTO.Data = null;
                            upsetCustomerBasketResponseDTO.Message = "Đã xóa giỏ hàng";
                            return upsetCustomerBasketResponseDTO;
                        }
                        else
                        {
                            upsetCustomerBasketResponseDTO.Data = await _repository.UpdateBasketItemAsync(upsetCustomerBasketDTO.CustomerId, upsetCustomerBasketDTO.ProductId, quantityUpdate);
                            upsetCustomerBasketResponseDTO.Message = "Đã giảm số lượng Item.";
                            return upsetCustomerBasketResponseDTO;
                        }
                    }
                }
                return null;
            }
            return null;
        }

        public async Task<bool> RemoveCustomerBasketByCustomerIdAsync(int customerId)
        {
            return await _repository.RemoveCustomerBasketByCustomerIdAsync(customerId);
        }
    }
}
