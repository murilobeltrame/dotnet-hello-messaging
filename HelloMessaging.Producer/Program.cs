using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;
using HelloMessaging.Domain;
using Serilog;

namespace HelloMessaging.Producer
{
    class Program
    {
        protected Program() {}
        
        static async Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();

            var sender = host.Services.GetRequiredService<IPublishEndpoint>();

            await host.StartAsync();

            while (true)
            {
                string value = await Task.Run(() =>
                {
                    Console.WriteLine("Type a message and hit enter to send, or `quit` to exit.");
                    Console.Write("> ");
                    return Console.ReadLine();
                });

                if ("quit".Equals(value, StringComparison.OrdinalIgnoreCase)) break;

                await sender.Publish<IChatMessage>(new { Text = value });
            }
        }

        static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .UseSerilog((host, log) =>
            {
                if (host.HostingEnvironment.IsProduction())
                    log.MinimumLevel.Information();
                else
                    log.MinimumLevel.Debug();
                log.WriteTo.Console();
            })
            .ConfigureServices(services =>
            {
                services
                    .AddMassTransit(configuration =>
                    {
                        configuration.UsingRabbitMq((context, config) =>
                        {
                            config.Host("amqp://guest:guest@localhost:5672");
                        });
                    })
                    .AddMassTransitHostedService();
            });
    }

}
