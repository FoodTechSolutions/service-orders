using APPLICATION.Order.CreateOrder;
using APPLICATION.Order.GetById;
using APPLICATION.Order.GetByIdAsync;
using APPLICATION.Order.GetOrder;
using APPLICATION.Order.GetOrdersGroupByStatus;
using MediatR;

namespace API.Configuration;

public static class ConfigurationMediator
{
    public static IServiceCollection AddInjectMediator(this IServiceCollection services)
    {

        services.AddScoped<IMediator, Mediator>();
        services.AddTransient<IRequestHandler<GetOrderQuery, IEnumerable<GetOrderResponse>>, GetOrderQueryHandler>();
        services.AddTransient<IRequestHandler<CreateOrderCommand, CreateOrderResponse>, CreateOrderCommandHandler>();
        services.AddTransient<IRequestHandler<GetOrderByIdQuery, GetOrderByIdResponse>, GetOrderByIdQueryHandler>();
        services.AddTransient<IRequestHandler<GetOrdersGroupByStatusQuery, GetOrdersGroupByStatusResponse>, GetOrdersGroupByStatusQueryHandler>();

        return services;
    }
}
