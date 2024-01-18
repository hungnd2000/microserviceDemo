using Basket.API.DTOs;
using Basket.API.Entities;

namespace Basket.API.Application.Repositories
{
    public interface ICustomerBasketRepository
    {
        //Customer's basket
        Task<List<CustomerBasket>> GetAllCustomerBasketAsync();
        Task<CustomerBasket> GetCustomerBasketByIdAsync(int id);
        Task<CustomerBasket> AddCustomerBasketAsync(CustomerBasket basket);
        Task<bool> RemoveCustomerBasketByCustomerIdAsync(int customerId);

        //Basket item
        Task<CustomerBasket> AddBasketItemAsync(CustomerBasket customerBasket);
        Task<CustomerBasket> UpdateBasketItemAsync(int customerId, int productId, int quantity);
    }
}
