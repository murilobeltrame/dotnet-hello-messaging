using System.Threading.Tasks;

namespace HelloMessaging.Lib
{
    public interface IBusService
    {
        IBusClient Client(string queuename);
    }
}