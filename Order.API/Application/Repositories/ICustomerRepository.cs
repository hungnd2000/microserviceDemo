using Order.API.Entities;

namespace Order.API.Application.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdentityAsync(int identity);
        Task<Customer> AddAsync(Customer customer);
    }
}
