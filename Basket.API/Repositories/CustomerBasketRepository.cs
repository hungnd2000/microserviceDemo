using Basket.API.Application.Repositories;
using Basket.API.Database;
using Basket.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Basket.API.Repositories
{
    public class CustomerBasketRepository : ICustomerBasketRepository
    {
        private readonly BasketDbContext _context;
        private readonly ILogger<CustomerBasketRepository> _logger;

        public CustomerBasketRepository(BasketDbContext context, ILogger<CustomerBasketRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        // Customer basket
        public async Task<CustomerBasket> AddCustomerBasketAsync(CustomerBasket basket)
        {
            try
            {
                foreach (var item in basket.Items)
                {
                    item.Id = Guid.NewGuid().ToString();
                }
                _context.CustomerBaskets.Add(basket);
                await _context.SaveChangesAsync();
                return basket;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }      
        }

        public async Task<List<CustomerBasket>> GetAllCustomerBasketAsync()
        {
            try
            {
                var listCustomerBasket = await _context.CustomerBaskets.ToListAsync();
                for (int i = 0; i < listCustomerBasket.Count;i++)
                {
                    await _context.Entry(listCustomerBasket[i]).Collection(i => i.Items).LoadAsync();
                }
                return listCustomerBasket;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<CustomerBasket> GetCustomerBasketByIdAsync(int id)
        {
            try
            {
                var result = await _context.CustomerBaskets.FirstOrDefaultAsync(e => e.CustomerId.Equals(id));
                if (result != null)
                {
                    await _context.Entry(result).Collection(i =>i.Items).LoadAsync();
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<CustomerBasket> AddBasketItemAsync(CustomerBasket cusBasket)
        {
            try
            {
                var customerBasketAdd = await GetCustomerBasketByIdAsync(cusBasket.CustomerId);
                if (customerBasketAdd != null)
                {
                    for (int i = 0; i < cusBasket.Items.Count; i++)
                    {
                        cusBasket.Items[i].Id = Guid.NewGuid().ToString();
                    }
                    customerBasketAdd.Items.AddRange(cusBasket.Items);
                    _context.CustomerBaskets.Update(customerBasketAdd);
                }
                await _context.SaveChangesAsync();
                return customerBasketAdd;
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message); 
                return null;
            }
        }

        public async Task<CustomerBasket> UpdateBasketItemAsync(int customerId, int productId, int quantity)
        {
            try
            {
                var customerBasket = await GetCustomerBasketByIdAsync(customerId);
                if (customerBasket != null)
                {
                    if (quantity != 0)
                    {
                        for (int i = 0; i < customerBasket.Items.Count; i++)
                        {
                            if (customerBasket.Items[i].ProductId == productId)
                            {
                                customerBasket.Items[i].Quantity = quantity;
                            }
                        }
                    }
                    else
                    {
                        for(int i=0; i<customerBasket.Items.Count; i++)
                        {
                            if (customerBasket.Items[i].ProductId == productId)
                            {
                                var itemRemove = customerBasket.Items[i];
                                customerBasket.Items.Remove(itemRemove);
                            }
                        }
                    }

                    _context.CustomerBaskets.Update(customerBasket);
                    await _context.SaveChangesAsync();
                    return customerBasket;
                }
                return null;
            }catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> RemoveCustomerBasketByCustomerIdAsync(int customerId)
        {
            try
            {
                var basketRemove = await GetCustomerBasketByIdAsync(customerId);
                _context.CustomerBaskets.Remove(basketRemove);
                await _context.SaveChangesAsync();
                return true;
            }catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
