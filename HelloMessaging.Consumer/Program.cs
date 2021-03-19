﻿using System;
using System.Threading.Tasks;
using HelloMessaging.Domain;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace HelloMessaging.Consumer
{
    class Program
    {
        protected Program() {}
        
        static async Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();
            await host.StartAsync();

            Console.WriteLine("Type any key to quit");
            Console.ReadKey();
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
                        configuration.AddConsumer<ChatMessageConsumer>();
                        configuration.UsingRabbitMq((context, config) =>
                        {
                            config.Host("amqp://guest:guest@localhost:5672");
                            config.ReceiveEndpoint("chat-service", c =>
                            {
                                c.ConfigureConsumer<ChatMessageConsumer>(context);
                            });
                        });
                    })
                    .AddMassTransitHostedService();
            });
    }

    class ChatMessageConsumer : IConsumer<ChatMessage>
    {
        private readonly ILogger<ChatMessageConsumer> _logger;

        public ChatMessageConsumer(ILogger<ChatMessageConsumer> logger) => _logger = logger;

        public Task Consume(ConsumeContext<ChatMessage> context)
        {
            var messageText = context.Message.Text;
            if (messageText.Contains("error", StringComparison.InvariantCultureIgnoreCase)) throw new Exception(messageText);
            _logger.LogInformation($"Chatty said: '{context.Message.Text}'");
            return Task.CompletedTask;
        }
    }
}
