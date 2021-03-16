using System.Threading.Tasks;

namespace HelloMessaging.Lib
{
    public interface IBusService
    {
        Task SendMessageAsync<T>(string queueName, T message);
    }
}