using DOMAIN.Enums;
using DOMAIN.Repository;
using INFRA.Repositories;
using MediatR;

namespace APPLICATION.Order.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICreateOrderQueueAdapterOUT _createOrderQueueAdapterOUT;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, ICreateOrderQueueAdapterOUT createOrderQueueAdapterOUT)
    {
        _orderRepository = orderRepository;
        _createOrderQueueAdapterOUT = createOrderQueueAdapterOUT;
    }

    public async Task<CreateOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {

        var order = DOMAIN.Order.CreateOrder(request.CustomerId, request.Discount, OrderStatus.Received);

        order.AddProducts(request.Product);

        await _orderRepository.AddAsync(order);
        _orderRepository.SaveChangesAsync();

        _createOrderQueueAdapterOUT.Publish(order);

        return CreateOrderResponse.ToResponse(order);
    }
}
