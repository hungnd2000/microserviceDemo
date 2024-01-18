using Order.API.Entities;

namespace Order.API.Application.Repositories
{
    public interface IOrderRepository
    {
        Task<List<OrderModel>> GetAllOrder();
        Task<OrderModel> GetOrderByCustomerId(int CustomerId);
        Task<OrderModel> AddOrder(OrderModel order);
        Task<bool> DeleteByCustomerIdAsync(int customerId);
    }
}
