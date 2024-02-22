
using Basket.API.Application.Services;
using Basket.API.DTOs;
using Basket.API.Services;
using Confluent.Kafka;
using Manonero.MessageBus.Kafka.Abstractions;
using System.Text.Json;

namespace Basket.API.BackgroundTasks
{
    public class ConsumerBackgroundTask : IConsumingTask<string, string>
    {

        private readonly ILogger<ConsumerBackgroundTask> _logger;
        private readonly IServiceProvider _serviceProvider;

        public ConsumerBackgroundTask(ILogger<ConsumerBackgroundTask> logger, IServiceProvider serviceProvider)
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
                    var customerBasketService = scope.ServiceProvider.GetRequiredService<ICustomerBasketService>();
                    // Use customerBasketService here
                    var messageValue = JsonSerializer.Deserialize<OrderDTO>(message);
                    var result = await customerBasketService.RemoveCustomerBasketByCustomerIdAsync(messageValue.CustomerId);
                    if (result == true)
                    {
                        _logger.LogInformation($"Received message: Success!");
                    }
                    else
                    {
                        _logger.LogInformation($"Received message: Fail!");
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
