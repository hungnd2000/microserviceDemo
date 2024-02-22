using Basket.API.DTOs;
using Confluent.Kafka;
using Manonero.MessageBus.Kafka.Abstractions;
using Product.API.Application.Services;
using Product.API.DTOs;
using System.Text.Json;

namespace Product.API.BackgroundTasks
{
    public class ConsumerBackgroundTask : IConsumingTask<string, string>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ConsumerBackgroundTask> _logger;

        public ConsumerBackgroundTask(IServiceProvider serviceProvider, ILogger<ConsumerBackgroundTask> logger)
        {
                _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public void Execute(ConsumeResult<string, string> result)
        {
            try
            {
                var message = result.Message.Value;
                // Process the Kafka message here
                ConsumerCallbackAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error consuming message: {ex}");
            }
        }

        public async void ConsumerCallbackAsync(string message)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var productService = scope.ServiceProvider.GetRequiredService<IProductService>();
                    // Use customerBasketService here
                    var messageValue = JsonSerializer.Deserialize<List<ProductUpdateQuantity>>(message);
                    foreach(var item in messageValue)
                    {
                        var product = await productService.GetProductByIdAsync(item.ProductId);
                        var productUpdateQuantity = new ProductUpdateQuantity()
                        {
                            ProductId = product.Id,
                            AvailableQuantity = product.AvailableQuantity - item.AvailableQuantity,
                        };

                        var result = await productService.UpdateAvailableQuantityAsync(productUpdateQuantity.ProductId, productUpdateQuantity.AvailableQuantity);
                        if (result != null)
                        {
                            _logger.LogInformation(message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
    }
}
