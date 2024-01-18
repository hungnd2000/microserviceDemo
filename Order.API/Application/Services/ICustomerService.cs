using Order.API.Entities;

namespace Order.API.Application.Services
{
    public interface ICustomerService
    {
        Task<Customer> GetByIdentityAsync(int identity);
        Task<Customer> AddAsync(Customer customer);
        void RemoveCustomerBasketAsync(int customerId);
    }
}
