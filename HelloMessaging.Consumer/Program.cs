using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace HelloMessaging.Consumer
{
    class Program
    {
        protected Program() {}
        
        static async Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args);
    }
}
