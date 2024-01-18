using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.API.Application.Repositories;
using Order.API.Application.Services;
using Order.API.DTOs;
using Order.API.Entities;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<List<OrderModel>> GetOrders()
        {
            return await _orderService.GetAllOrderAsync();
        }

        [HttpPost]
        public async Task<UpsertOrderResponse> AddAsync(UpsertOrder upsertOrder)
        {
            return await _orderService.AddAsync(upsertOrder);
        }

        //[HttpDelete]
        //[Route("{customerId}")]
        //public async Task<bool> RemoveOrderByCustomerId(int customerId)
        //{
        //    return await _orderService.DeleteByCustomerIdAsync(customerId);
        //}
    }
}
