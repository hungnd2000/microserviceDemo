using Order.API.Entities;

namespace Order.API.Application.Repositories
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItem>> GetAllOrderItem();
        Task<OrderItem> GetOrderItemById(string id);
        Task<OrderItem> AddOrderItem(OrderItem item);
        Task<OrderItem> UpdateOrderItemById(string id, OrderItem item);
        Task<bool> DeleteOrderItem(string id);
    }
}
