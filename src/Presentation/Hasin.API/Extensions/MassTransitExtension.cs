using EventBus.Common;
using Hasin.API.EventBusConsumer;
using MassTransit;

namespace Hasin.API.Extensions
{
    public static class MassTransitExtension
    {
        public static IServiceCollection ConfigureMassTransit(this IServiceCollection services, ConfigurationManager configuration)
        {

            services.AddMassTransit(config =>
            {

                config.AddConsumer<EventBusBookConsumer>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["EventBusSettings:HostAddress"]);
                    cfg.ReceiveEndpoint(EventBusConstant.HASIN_QUEUE, c =>
                    {
                        c.ConfigureConsumer<EventBusBookConsumer>(ctx);
                    });
                });

            });
            services.AddMassTransitHostedService();
            return services;
        }
    }
}
