using System;
using System.Threading;
using System.Threading.Tasks;

namespace HelloMessaging.Lib
{
    public interface IBusClient {
        
        Task CompleteAsync(string lockToken);
        Task CloseAsync();
        void RegisterMessageHandler<T>(Func<IBusMessage<T>, IBusClient, CancellationToken, Task> handler, Func<Exception, Task> exceptionHandler);
        Task SendMessageAsync<T>(T message);
    }
}