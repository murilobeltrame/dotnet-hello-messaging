using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace HelloMessaging.Lib
{
    public class AzureBusService : IBusService
    {
        private readonly IConfiguration _configuration;

        public AzureBusService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMessageAsync<T>(string queueName, T message)
        {
            var client = new QueueClient(_configuration.GetConnectionString("AzureServiceBus"), queueName);
            var messageBody = JsonSerializer.Serialize(message);
            var queueMessage = new Message(Encoding.UTF8.GetBytes(messageBody));
            await client.SendAsync(queueMessage);
        }
    }
}