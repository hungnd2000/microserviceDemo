using Basket.API.Application.Services;
using Basket.API.DTOs;
using Basket.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerBasketController : ControllerBase
    {
        private readonly ICustomerBasketService _service;

        public CustomerBasketController(ICustomerBasketService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<CustomerBasket>> GetAllCustomerBasket()
        {
            return await _service.GetAllCustomerBasketAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<CustomerBasket> GetBasketById(int id)
        {
            return await _service.GetBasketByIdAsync(id);
        }

        [HttpPost]
        public async Task<UpsetCustomerBasketResponseDTO> AddAsync(UpsetCustomerBasketDTO upsetCustomerBasketDTO)
        {
            return await _service.AddAsync(upsetCustomerBasketDTO);
        }


        [HttpPost]
        [Route("IncreaseItemQuantity")]
        public async Task<UpsetCustomerBasketResponseDTO> IncreaseItemQuantityAsync(UpsetCustomerBasketDTO upsetCustomerBasketDTO)
        {
            return await _service.IncreaseItemQuantityAsync(upsetCustomerBasketDTO);
        }

        [HttpPost]
        [Route("ReduceItemQuantity")]
        public async Task<UpsetCustomerBasketResponseDTO> ReduceItemQuantityAsync(UpsetCustomerBasketDTO upsetCustomerBasketDTO)
        {
            return await _service.ReduceItemQuantityAsync(upsetCustomerBasketDTO);
        }

        [HttpDelete]
        [Route("{customerId}")]
        public async Task<bool> RemoveCustomerBasketByCustomerId(int customerId)
        {
            return await _service.RemoveCustomerBasketByCustomerIdAsync(customerId);
        }
    }
}
