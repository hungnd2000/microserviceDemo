using Confluent.Kafka;
using Manonero.MessageBus.Kafka.Abstractions;
using Order.API.Application.Repositories;
using Order.API.Application.Services;
using Order.API.DTOs;
using Order.API.Entities;
using System.Text.Json;

namespace Order.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly IOrderRepository _repository;
        private readonly ILogger<OrderService> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient _client;
        public readonly IKafkaProducerManager _producerManager;

        public OrderService(
            IProductService productService,
            ICustomerService customerService,
            IOrderRepository repository,
            ILogger<OrderService> logger,
            IConfiguration config,
            HttpClient client,
            IKafkaProducerManager producerManager
            )
        {
            _productService = productService;
            _customerService = customerService;
            _repository = repository;
            _logger = logger;
            _config = config;
            _client = client;
            _producerManager = producerManager;
        }
        public async Task<UpsertOrderResponse> AddAsync(UpsertOrder upsertOrder)
        {
            string ApiGetCustomerBasketById = _config["HttpGetCustomerBasket"] + "/" + upsertOrder.IdentityId;
            HttpResponseMessage response = new HttpResponseMessage();
            OrderModel order = new OrderModel();
            UpsertOrderResponse upsertOrderResponse = new UpsertOrderResponse("", order);
            List<ProductUpdateQuantity> productUpdateQuantities = new List<ProductUpdateQuantity>();

            response = await _client.GetAsync(ApiGetCustomerBasketById);
            if (response.IsSuccessStatusCode)
            {
                if (response.Content.Headers.ContentLength != 0)
                {
                    var customerBasket = await response.Content.ReadFromJsonAsync<CustomerBasket>();
                    if (customerBasket.Items.Count() != 0)
                    {
                        order.OrderDate = DateTime.Now;
                        order.Street = upsertOrder.Street;
                        order.District = upsertOrder.District;
                        order.City = upsertOrder.City;
                        order.AdditionalAddress = upsertOrder.AdditionalAddress;
                        order.CustomerId = upsertOrder.IdentityId;
                        foreach (var item in customerBasket.Items)
                        {
                            if (item.Status == 1)
                            {
                                var productUpdateQuantity = new ProductUpdateQuantity();
                                OrderItem orderItem = new OrderItem();
                                orderItem.ProductId = item.ProductId;
                                orderItem.ProductName = item.ProductName;
                                orderItem.Quantity = item.Quantity;

                                order.Items.Add(orderItem);
                                productUpdateQuantity.ProductId = item.ProductId;
                                productUpdateQuantity.AvailableQuantity = item.Quantity;
                                productUpdateQuantities.Add(productUpdateQuantity);
                            }
                        }

                        var orderResult = await _repository.AddOrder(order);
                        if (orderResult != null)
                        {

                            //_productService.UpdateProductQuantity(productUpdateQuantities);
                            //_customerService.RemoveCustomerBasketAsync(order.CustomerId);

                            ProduceBasketEvent(orderResult);
                            ProduceProductEvent(productUpdateQuantities);
                            upsertOrderResponse.Data = orderResult;
                            upsertOrderResponse.Message = "Thêm mới thành công";
                            return upsertOrderResponse;
                        }
                    }
                }
            }
            upsertOrderResponse.Message = "Thêm thất bại";
            upsertOrderResponse.Data = null;
            return upsertOrderResponse;
        }

        public async Task<bool> DeleteByCustomerIdAsync(int customerId)
        {
            return await _repository.DeleteByCustomerIdAsync(customerId);
        }

        public async Task<List<OrderModel>> GetAllOrderAsync()
        {
            return await _repository.GetAllOrder();
        }

        public Task<OrderModel> GetByIdAsync(string orderId)
        {
            throw new NotImplementedException();
        }

        //public void ProduceEvent(IntegrationEvent @event)
        //{
        //    var json = JsonSerializer.Serialize(@event, @event.GetType(), new JsonSerializerOptions
        //    {
        //        WriteIndented = true,
        //    });

        //    var message = new Message<Null, string> { Value = json };
        //    _kafkaProducer.Produce(message, "ProductCommand");
        //}

        private void ProduceBasketEvent(OrderModel orderResult)
        {
            var json = JsonSerializer.Serialize(orderResult);
            var message = new Message<string, string> { Value = json };

            var kafkaProducer = _producerManager.GetProducer<string, string>("Order");
            kafkaProducer.Produce(message);
            _logger.LogInformation($"Received message: {message}");
        }

        private void ProduceProductEvent(List<ProductUpdateQuantity> orderResult)
        {
            var json = JsonSerializer.Serialize(orderResult);
            var message = new Message<string, string> { Value = json };

            var kafkaProducer = _producerManager.GetProducer<string, string>("Order_Result");
            kafkaProducer.Produce(message);
            _logger.LogInformation($"Received message: {message}");
        }
    }
}
