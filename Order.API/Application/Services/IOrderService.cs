using Order.API.DTOs;
using Order.API.Entities;

namespace Order.API.Application.Services
{
    public interface IOrderService
    {
        Task<UpsertOrderResponse> AddAsync(UpsertOrder upsertOrder);
        Task<bool> DeleteByCustomerIdAsync(int customerId);
        Task<List<OrderModel>> GetAllOrderAsync();
        Task<OrderModel> GetByIdAsync(string orderId);
    }
}
