using System.Text;
using System.Text.Json;
using Microsoft.Azure.ServiceBus;

namespace HelloMessaging.Lib
{
    public class AzureBusMessage<T> : AzureBusMessage, IBusMessage<T>
    {
        public T body { get; set; }
        public string lockToken { get; set; }
    }

    public abstract class AzureBusMessage {
        public static AzureBusMessage<T> FromMessage<T>(Message message) {
            var messageBody = Encoding.UTF8.GetString(message.Body);
            var obj = JsonSerializer.Deserialize<T>(messageBody);
            return new AzureBusMessage<T>{ body = obj, lockToken = message.SystemProperties.LockToken};
        }
    }
}