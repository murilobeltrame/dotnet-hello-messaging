using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace HelloMessaging.Lib
{
    public class AzureBusService : IBusService
    {
        private readonly IConfiguration _configuration;
        private Dictionary<string, QueueClient> _clients;

        public AzureBusService(/*IConfiguration configuration*/)
        {
            // _configuration = configuration;
            _clients = new Dictionary<string, QueueClient>();
        }

        public IBusClient Client(string queuename) => new AzureBusClient(GetQueueClientInstance(queuename));

        private QueueClient GetQueueClientInstance(string queueName) {
            if (!_clients.ContainsKey(queueName)) _clients.Add(queueName, new QueueClient(_configuration.GetConnectionString("AzureServiceBus"), queueName));
            return _clients[queueName];
        }
    }
}