using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace HelloMessaging.Lib
{
    public class AzureBusClient: IBusClient {
        private readonly QueueClient _clientInstance;

        public AzureBusClient(QueueClient clientInstance)
        {
            _clientInstance = clientInstance;
        }

        public async Task CloseAsync()
        {
            await _clientInstance.CloseAsync();
        }

        public async Task CompleteAsync(string lockToken)
        {
            await _clientInstance.CompleteAsync(lockToken);
        }

        public void RegisterMessageHandler<T>(Func<IBusMessage<T>, IBusClient, CancellationToken, Task> handler, Func<Exception, Task> exceptionHandler)
        {
            _clientInstance.RegisterMessageHandler(async (Message, CancellationToken) => {
                var busMessage = AzureBusMessage.FromMessage<T>(Message);
                await handler(busMessage, this, CancellationToken);
            }, async (ExceptionReceivedEventArgs) => {
                await exceptionHandler(ExceptionReceivedEventArgs.Exception);
            });
        }

        public async Task SendMessageAsync<T>(T message)
        {
            var messageBody = JsonSerializer.Serialize(message);
            var queueMessage = new Message(Encoding.UTF8.GetBytes(messageBody));
            await _clientInstance.SendAsync(queueMessage);
        }
    }
}