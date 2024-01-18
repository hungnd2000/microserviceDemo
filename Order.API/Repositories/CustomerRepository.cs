using Order.API.Application.Repositories;
using Order.API.Database;
using Order.API.Entities;

namespace Order.API.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly OrderDbContext _db;
        private readonly ILogger<CustomerRepository> _logger;
        public CustomerRepository(OrderDbContext db, ILogger<CustomerRepository> logger)
        {
            _db = db;
            _logger = logger;
        }
        public async Task<Customer> AddAsync(Customer customer)
        {
            try
            {
                _db.Customers.AddAsync(customer);
                await _db.SaveChangesAsync();
                return customer;
            }catch(Exception e)
            {
                _logger.LogError(e.ToString());
                return null;
            }
        }

        public Task<Customer> GetByIdentityAsync(int identity)
        {
            try
            {
                var result = _db.Customers.Find(identity);
                return Task.FromResult(result);
            }catch(Exception e)
            {
                _logger.LogError(e.ToString());
                return null;
            }
        }
    }
}
