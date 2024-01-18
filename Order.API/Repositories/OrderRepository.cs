using Microsoft.EntityFrameworkCore;
using Order.API.Application.Repositories;
using Order.API.Database;
using Order.API.Entities;
using System.IO;
using System.Linq.Expressions;

namespace Order.API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;
        private readonly ILogger<OrderRepository> _logger;
        public OrderRepository(OrderDbContext context, ILogger<OrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<OrderModel> AddOrder(OrderModel order)
        {
            try
            {   
                order.Id = Guid.NewGuid().ToString();
                foreach (var item in order.Items)
                {
                    item.Id = Guid.NewGuid().ToString();
                }
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<bool> DeleteByCustomerIdAsync(int CustomerId)
        {
            try
            {
                var orderDel = await GetOrderByCustomerId(CustomerId);
                if (orderDel != null)
                {
                    _context.Orders.Remove(orderDel);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        public async Task<List<OrderModel>> GetAllOrder()
        {
            try
            {
                var AllOrder = await _context.Orders.ToListAsync();
                for (int i = 0; i < AllOrder.Count; i++)
                {
                    await _context.Entry(AllOrder[i]).Collection(i => i.Items).LoadAsync();
                }
                return AllOrder;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<OrderModel> GetOrderByCustomerId(int customerId)
        {
            try
            {
                return await _context.Orders.FirstOrDefaultAsync(e => e.CustomerId == customerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}
