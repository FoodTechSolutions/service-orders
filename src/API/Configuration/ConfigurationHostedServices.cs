using APPLICATION.BackgroundServices;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace API.Configuration;

public static class ConfigurationHostedServices
{
    public static IServiceCollection AddHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<RabbitMqExampleHandler>();

        return services;
    }
}

