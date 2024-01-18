using Basket.API.DTOs;
using Basket.API.Entities;

namespace Basket.API.Application.Services
{
    public interface ICustomerBasketService
    {
        Task<List<CustomerBasket>> GetAllCustomerBasketAsync();
        Task<UpsetCustomerBasketResponseDTO> AddAsync(UpsetCustomerBasketDTO upsetCustomerBasketDTO);
        Task<CustomerBasket> GetBasketByIdAsync(int id);
        Task<bool> RemoveCustomerBasketByCustomerIdAsync(int customerId);
    }
}
