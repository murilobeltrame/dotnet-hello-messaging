using System;
using System.Threading.Tasks;
using HelloMessaging.Lib;

namespace HelloMessaging.Consumer
{
    class Program
    {
        const string connectionString = "";
        const string queueMessage = "";

        static async Task Main(string[] args)
        {
            var _client = new AzureBusService().Client("chatting");
            _client.RegisterMessageHandler<string>(async (queueMessage, client, cancelationToken) => {
                Console.WriteLine(queueMessage);
                await client.CompleteAsync(queueMessage.lockToken);
            }, (exception) => {
                Console.WriteLine(exception.Message);
                return Task.CompletedTask;
            });

            Console.WriteLine("Press any key to stop!");
            Console.ReadKey();
            await _client.CloseAsync();
        }
    }
}
