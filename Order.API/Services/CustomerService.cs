using Order.API.Application.Repositories;
using Order.API.Application.Services;
using Order.API.DTOs;
using Order.API.Entities;
using System.Text.Json;
using System.Text;

namespace Order.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomerService> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient _client;
        public CustomerService
            (
            ICustomerRepository repository, 
            ILogger<CustomerService> logger, 
            IConfiguration config,
            HttpClient client
            )
        {
            _repository = repository;
            _logger = logger;
            _config = config;
            _client = client;
        }
        public async Task<Customer> AddAsync(Customer customer)
        {
            try
            {
                return await _repository.AddAsync(customer);
            }catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<Customer> GetByIdentityAsync(int identity)
        {
            try
            {
                return await _repository.GetByIdentityAsync(identity);
            }catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public async void RemoveCustomerBasketAsync(int customerId)
        {
            try
            {
                string deleteUrl = _config["HttpGetCustomerBasket"] + "/" + customerId;
                await _client.DeleteAsync(deleteUrl);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
    }
}
