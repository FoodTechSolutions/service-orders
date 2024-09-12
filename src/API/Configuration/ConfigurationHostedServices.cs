using APPLICATION.BackgroundServices;
using APPLICATION.Messaging;
using DOMAIN.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace API.Configuration;

public static class ConfigurationHostedServices
{
    public static IServiceCollection AddHostedServices(this IServiceCollection services)
    {
        //services.AddHostedService<RabbitMqExampleHandler>();
        services.AddHostedService<CancelOrderQueueAdapterIN>();
        services.AddHostedService<PaidQueueAdpterIN>();
        services.AddHostedService<FinishOrderQueueAdapterIN>();

        return services;
    }
}

